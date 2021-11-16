#include "MainProcess.h"
#include <Adafruit_MCP23017.h>
#include "hmi.h"

extern HardwareSerial *cmd_port;
MainDataStruct maindata;
RuntimeStatus runtimedata;
DigitalIO digitalio;
Adafruit_MCP23017 extio[EXTIO_NUM];

void MainProcess_ReCheckEEPROMValue()
{
	if((maindata.HMI_ID < 0) || (maindata.HMI_ID > 128))
	{
		maindata.HMI_ID = 0;
		runtimedata.UpdateEEPROM = true;
	}
}

void MainProcess_Init()
{
	int i,j;
	runtimedata.UpdateEEPROM = false;
	MainProcess_ReCheckEEPROMValue();

	for(i=0;i<INPUT_8_NUMBER+EXTIO_NUM;i++)
		digitalio.Input[i] = 0;
	
	for(i=0;i<OUTPUT_8_NUMBER+EXTIO_NUM;i++)
	{
		if(OUTPUT_NONE_ACTIVE == 0)
			digitalio.Output[i]	= 0;
		else
			digitalio.Output[i]	= 0xFF;
	}
		
	for(i=0; i<INPUT_8_NUMBER*8; i++)
	{
		pinMode(InputPin[i], INPUT);
	}
	for(i=0; i<OUTPUT_8_NUMBER*8; i++)
	{
		pinMode(OutputPin[i], OUTPUT);	
	}
	
	for(j=0; j<EXTIO_NUM; j++)
	{
		extio[j].begin(j);	  	// Default device address 0x20+j

		for(i=0; i<8; i++)
		{
			extio[j].pinMode(i, OUTPUT);  // Toggle LED 1
			extio[j].digitalWrite(i, OUTPUT_NONE_ACTIVE);
		}
	}
	for(i=0; i<OUTPUT_8_NUMBER*8; i++)
		digitalWrite(OutputPin[i], OUTPUT_NONE_ACTIVE);

	for(j=0; j<EXTIO_NUM; j++)
		for(i=0; i<8; i++)
		{
			extio[j].pinMode(i+8,INPUT);	 // Button i/p to GND
			extio[j].pullUp(i+8,HIGH);	 // Puled high to ~100k
		}
    for(i=0; i< WORKINDEX_TOTAL; i++)
        runtimedata.Workindex[i] = 0;    
    for(i=0; i< WORKINDEX_TOTAL; i++)
        runtimedata.preWorkindex[i] = -1;
    Device_Stop();
}

void ReadDigitalInput()
{
	uint8_t i,bi, j, value;
	String outstr = "";
	bool inputupdate = false;
	uint8_t input8 = 1;
	
	for(i=0; i<8; i++)
	{
		runtimedata.sensor[i] = digitalRead(InputPin[i]);
		if(runtimedata.sensor[i])
			{setbit(digitalio.Input[0], i);	}
		else
			{clrbit(digitalio.Input[0], i);	}
	}

	if(INPUT_8_NUMBER == 2)
	{
		for(i=0; i<8; i++)
		{
			runtimedata.sensor[i+8] = digitalRead(InputPin[i+8]);
			
			if(runtimedata.sensor[i+8])
				{setbit(digitalio.Input[1], i); }
			else
				{clrbit(digitalio.Input[1], i); }
		}
		input8 += 1;
	}

	if(EXTIO_NUM > 0)
	{
		for(i=0; i<8; i++)
		{
			runtimedata.sensor[i+8] = extio[0].digitalRead(i+8);
				
			if(runtimedata.sensor[i+input8*8])
				{setbit(digitalio.Input[input8], i);	}
			else
				{clrbit(digitalio.Input[input8], i);	}
		}
		input8 += 1;
	}
	if(EXTIO_NUM > 1)
	{
		for(i=0; i<8; i++)
		{
			runtimedata.sensor[i+input8*8] = extio[1].digitalRead(i+8);
			if(runtimedata.sensor[i+input8*8])
				{setbit(digitalio.Input[input8], i);	}
			else
				{clrbit(digitalio.Input[input8], i);	}
		}
	}

}

void WriteDigitalOutput()
{
	uint8_t i,bi, j, value;

	for(i=0; i<OUTPUT_8_NUMBER+EXTIO_NUM; i++)
	{
		if(digitalio.PreOutput[i] != digitalio.Output[i])
		{
			digitalio.PreOutput[i] = digitalio.Output[i];
			
			switch(i)
			{
				case 0: //onboard
					for(bi=0; bi<8; bi++)
					{
						value = getbit(digitalio.Output[i], bi);
						digitalWrite(OutputPin[bi], value);
					}
					break;

				case 1: //extern board 0
					for(bi=0; bi<8; bi++)
					{
						value = getbit(digitalio.Output[i], bi);
						if(OUTPUT_8_NUMBER == 2)
							digitalWrite(OutputPin[bi+8], value);
						else
							extio[0].digitalWrite(bi, value);
					}
					break;
				case 2: //extern board 1
					for(bi=0; bi<8; bi++)
					{
						value = getbit(digitalio.Output[i], bi);
						if(OUTPUT_8_NUMBER == 2)
							extio[0].digitalWrite(bi, value);
						else
							extio[1].digitalWrite(bi, value);
					}
					break;
				case 3: //extern board 1
					for(bi=0; bi<8; bi++)
					{
						value = getbit(digitalio.Output[i], bi);
						extio[1].digitalWrite(bi, value);
					}
					break;
			}	
		}
	}
}

void setOutput(uint8_t index, uint8_t hl)
{
	if(index < (OUTPUT_8_NUMBER*8))
	{
		digitalWrite(OutputPin[index], hl);
	}
	else
	{
		uint8_t extindex = index-(OUTPUT_8_NUMBER*8);
		uint8_t exi = extindex >> 3;
		uint8_t bi = extindex & 0x07;
		extio[exi].digitalWrite(bi, hl);
	}
	digitalio.Output[index] = hl;
}

uint8_t getInput(uint8_t index)
{
	uint8_t hl;
	if(index < (INPUT_8_NUMBER*8))
	{
		hl = digitalRead(InputPin[index]);
	}
	else
	{
		uint8_t extindex = index-(INPUT_8_NUMBER*8);
		uint8_t exi = extindex >> 3;
		uint8_t bi = extindex & 0x07;
		hl = extio[exi].digitalRead(bi+8);
	}

	digitalio.Input[index] = hl;
	return hl;
}


void MainProcess_Task()
{
    if(runtimedata.preRunMode != runtimedata.RunMode)
    {
        runtimedata.preRunMode = runtimedata.RunMode;
        DEBUG("RunMode: " + String(runtimedata.preRunMode));
    }
	switch(runtimedata.RunMode)
    {
        case RUN_MODE_EMERGENCY_STOP:
            runtimedata.system_status = Emergency_Stop;
            Device_Stop();
            if(getInput(IN0_EMERGENCY) && runtimedata.Virtual_Emergency) //解除急停開關
            {
                runtimedata.RunMode = RUN_MODE_INIT;
                runtimedata.Workindex[WORKINDEX_INIT] = 0;
            }
            break;
        case RUN_MODE_INIT:
            runtimedata.system_status = Init;
            if(Device_Init())
            {
                runtimedata.RunMode = RUN_MODE_NORMAL;
                runtimedata.Workindex[WORKINDEX_NORMAL] = 0;
            }
            break;
        case RUN_MODE_NORMAL:
            NormalProcess();
            Btn345_Process();
            break;
    }   
    
}

#if 0//20211116 進行修改先保留舊程式
void NormalProcess()
{
    if(runtimedata.preWorkindex[WORKINDEX_NORMAL] != runtimedata.Workindex[WORKINDEX_NORMAL])
    {
        runtimedata.preWorkindex[WORKINDEX_NORMAL] = runtimedata.Workindex[WORKINDEX_NORMAL];
        DEBUG("WORKINDEX_NORMAL: " + String(runtimedata.preWorkindex[WORKINDEX_NORMAL]));
    }
    switch(runtimedata.Workindex[WORKINDEX_NORMAL])
    {
        case 0:
            Device_Stop();
            for(int i=0; i<3; i++)
                runtimedata.Sensor_345_State[i] = 0;
            if(!getInput(IN1_1_SWITCH) && !runtimedata.Virtual_Btn[IN1_1_SWITCH])
            {
                runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
                runtimedata.system_status = Normal_Free;
            }
            break;
        case 10:
            if(getInput(IN10_3_SENEOR) && getInput(IN11_4_SENEOR) && getInput(IN12_5_SENEOR))
                if(getInput(IN1_1_SWITCH) || runtimedata.Virtual_Btn[IN1_1_SWITCH])
                {
                    runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
                    runtimedata.system_status = Normal_Btn_1;
                }
            break;
        case 20://按鈕1按下前進/放開停止直到碰到1_FRONT_SENEOR
            if(getInput(IN1_1_SWITCH) || runtimedata.Virtual_Btn[IN1_1_SWITCH]){
                setOutput(OUT1_SOL1_BACK, 0);
                setOutput(OUT0_SOL1_FRONT, 1);
            }
            else{
                setOutput(OUT0_SOL1_FRONT, 0);
                setOutput(OUT1_SOL1_BACK, 0);

            }
            if(getInput(IN6_1_FRONT_SENEOR) && !getInput(IN7_1_BACK_SENEOR)){
                if(!getInput(IN1_1_SWITCH) && !runtimedata.Virtual_Btn[IN1_1_SWITCH])
                {
                    setOutput(OUT0_SOL1_FRONT, 0);
                    setOutput(OUT1_SOL1_BACK, 0);
                    runtimedata.Workindex[WORKINDEX_NORMAL] += 10; 
//                    runtimedata.system_status = Normal_Btn_2;
                }
            }
            break;
        case 30://目前在1_FRONT_SENEOR，並確認是否按下按鈕1或是按鈕2
            if(getInput(IN1_1_SWITCH) || runtimedata.Virtual_Btn[IN1_1_SWITCH]){
                runtimedata.Workindex[WORKINDEX_NORMAL] = 40;
            }
            if(getInput(IN2_2_SWITCH) || runtimedata.Virtual_Btn[IN2_2_SWITCH]){
                runtimedata.Workindex[WORKINDEX_NORMAL] = 60;
            }
            break;
        case 40://按下按鈕1後退/放開停止，直到碰到1_BACK_SENEOR且放開按鈕1
            if(getInput(IN1_1_SWITCH) || runtimedata.Virtual_Btn[IN1_1_SWITCH]){
                setOutput(OUT0_SOL1_FRONT, 0);
                setOutput(OUT1_SOL1_BACK, 1);
            }
            else{
                setOutput(OUT0_SOL1_FRONT, 0);
                setOutput(OUT1_SOL1_BACK, 0);

            }
            if(!getInput(IN6_1_FRONT_SENEOR) && getInput(IN7_1_BACK_SENEOR)){
                if(!getInput(IN1_1_SWITCH) && !runtimedata.Virtual_Btn[IN1_1_SWITCH])
                {
                    setOutput(OUT0_SOL1_FRONT, 0);
                    setOutput(OUT1_SOL1_BACK, 0);
                    runtimedata.Workindex[WORKINDEX_NORMAL] += 10; 
//                    runtimedata.system_status = Normal_Btn_2;
                }
            }
            break;
        case 50://目前在1_BACK_SENEOR，並確認是否按下按鈕1
            if(getInput(IN1_1_SWITCH) || runtimedata.Virtual_Btn[IN1_1_SWITCH]){
                runtimedata.Workindex[WORKINDEX_NORMAL] = 20;
            }
        //============================================================================
        case 60://按鈕2按下前進/放開停止直到碰到2_FRONT_SENEOR
            if(getInput(IN2_2_SWITCH) || runtimedata.Virtual_Btn[IN2_2_SWITCH]){
                setOutput(OUT3_SOL2_BACK, 0);
                setOutput(OUT2_SOL2_FRONT, 1);
            }
            else{
                setOutput(OUT2_SOL2_FRONT, 0);
                setOutput(OUT3_SOL2_BACK, 0);
            }
            if(getInput(IN8_2_FRONT_SENEOR) && !getInput(IN9_2_BACK_SENEOR)){
                if(!getInput(IN2_2_SWITCH) && !runtimedata.Virtual_Btn[IN2_2_SWITCH])
                {
                    setOutput(OUT2_SOL2_FRONT, 0);
                    setOutput(OUT3_SOL2_BACK, 0);
                    runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
                }
            }
            break;
        case 70://目前在2_FRONT_SENEOR，並確認是否按下按鈕2或是按鈕3
            if(getInput(IN2_2_SWITCH) || runtimedata.Virtual_Btn[IN2_2_SWITCH]){
                runtimedata.Workindex[WORKINDEX_NORMAL] = 80;
            }
            if(getInput(IN3_3_SWITCH) || runtimedata.Virtual_Btn[IN3_3_SWITCH]){
                runtimedata.Workindex[WORKINDEX_NORMAL] = 100;
            }
            break;
        case 80://按鈕2按下後退/放開停止直到碰到2_BACK_SENEOR
            if(getInput(IN2_2_SWITCH) || runtimedata.Virtual_Btn[IN2_2_SWITCH]){
                setOutput(OUT2_SOL2_FRONT, 0);
                setOutput(OUT3_SOL2_BACK, 1);
            }
            else{
                setOutput(OUT2_SOL2_FRONT, 0);
                setOutput(OUT3_SOL2_BACK, 0);
            }
            if(!getInput(IN8_2_FRONT_SENEOR) && getInput(IN9_2_BACK_SENEOR)){
                if(!getInput(IN2_2_SWITCH) && !runtimedata.Virtual_Btn[IN2_2_SWITCH])
                {
                    setOutput(OUT2_SOL2_FRONT, 0);
                    setOutput(OUT3_SOL2_BACK, 0);
                    runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
                }
            }
            break;
        case 90://目前在2_BACK_SENEOR，並確認是否按下按鈕2
            if(getInput(IN2_2_SWITCH) || runtimedata.Virtual_Btn[IN2_2_SWITCH]){
                runtimedata.Workindex[WORKINDEX_NORMAL] = 60;
            }
            break;
        case 100:
            /*
            按鈕3、4、5按下前進，放開後退，只要都使3、4、5 SENEOR改變過狀態
            即可按下按鈕2進行復歸流程
            */
            if(getInput(IN3_3_SWITCH) || runtimedata.Virtual_Btn[IN3_3_SWITCH]){
                setOutput(OUT5_SOL3_BACK, 0);
                setOutput(OUT4_SOL3_FRONT, 1);
                runtimedata.system_status = Normal_Btn_3;
            }
            else{
                setOutput(OUT4_SOL3_FRONT, 0);
                setOutput(OUT5_SOL3_BACK, 1);
                runtimedata.system_status = Normal_Btn_3;
            }
            //-----------------------------------
            if(getInput(IN4_4_SWITCH) || runtimedata.Virtual_Btn[IN4_4_SWITCH]){
                setOutput(OUT7_SOL4_BACK, 0);
                setOutput(OUT6_SOL4_FRONT, 1);
                runtimedata.system_status = Normal_Btn_4;
            }
            else{
                setOutput(OUT6_SOL4_FRONT, 0);
                setOutput(OUT7_SOL4_BACK, 1);
                runtimedata.system_status = Normal_Btn_4;
            }
            //-----------------------------------
            if(getInput(IN5_5_SWITCH) || runtimedata.Virtual_Btn[IN5_5_SWITCH]){
                setOutput(OUT9_SOL5_BACK, 0);
                setOutput(OUT8_SOL5_FRONT, 1);
                runtimedata.system_status = Normal_Btn_5;
            }
            else{
                setOutput(OUT8_SOL5_FRONT, 0);
                setOutput(OUT9_SOL5_BACK, 1);
                runtimedata.system_status = Normal_Btn_5;
            }
            //-----------------------------------
            if(!getInput(IN10_3_SENEOR)) {
                runtimedata.Sensor_345_State[0] = 1;
//                DEBUG(getInput(IN10_3_SENEOR));
            }
            if(!getInput(IN11_4_SENEOR)) {
                runtimedata.Sensor_345_State[1] = 1;
//                DEBUG(getInput(IN11_4_SENEOR));
            }
            if(!getInput(IN12_5_SENEOR)) {
                runtimedata.Sensor_345_State[2] = 1;
//                DEBUG(getInput(IN12_5_SENEOR));
            }
            if(runtimedata.Sensor_345_State[0] && runtimedata.Sensor_345_State[1] && runtimedata.Sensor_345_State[2])
            {
                if(!getInput(IN5_5_SWITCH) && !runtimedata.Virtual_Btn[IN5_5_SWITCH]
                    && !getInput(IN4_4_SWITCH) && !runtimedata.Virtual_Btn[IN4_4_SWITCH]
                    && !getInput(IN3_3_SWITCH) && !runtimedata.Virtual_Btn[IN3_3_SWITCH])
                    if(getInput(IN10_3_SENEOR) && getInput(IN11_4_SENEOR) && getInput(IN12_5_SENEOR))
                        if(getInput(IN2_2_SWITCH) || runtimedata.Virtual_Btn[IN2_2_SWITCH])
                        {
                            Device_Stop();
                            runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
                            runtimedata.system_status = Normal_Back_Btn_2;
                        }
            }
            break;
       //=======================================================================================
        case 110:
            if(getInput(IN2_2_SWITCH) || runtimedata.Virtual_Btn[IN2_2_SWITCH]){
                setOutput(OUT2_SOL2_FRONT, 0);
                setOutput(OUT3_SOL2_BACK, 1);
            }
            else{
                setOutput(OUT3_SOL2_BACK, 0);
                setOutput(OUT2_SOL2_FRONT, 0);
            }
            if(!getInput(IN8_2_FRONT_SENEOR) && getInput(IN9_2_BACK_SENEOR)){
                if(!getInput(IN2_2_SWITCH) && !runtimedata.Virtual_Btn[IN2_2_SWITCH])
                {
                    setOutput(OUT2_SOL2_FRONT, 0);
                    setOutput(OUT3_SOL2_BACK, 0);
                    runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
                    runtimedata.system_status = Normal_Back_Btn_1;
                }
            }
            break;
        case 120:
            if(getInput(IN1_1_SWITCH) || runtimedata.Virtual_Btn[IN1_1_SWITCH]){
                setOutput(OUT0_SOL1_FRONT, 0);
                setOutput(OUT1_SOL1_BACK, 1);
            }
            else{
                setOutput(OUT1_SOL1_BACK, 0);
                setOutput(OUT0_SOL1_FRONT, 0);
            }
            if(!getInput(IN6_1_FRONT_SENEOR) && getInput(IN7_1_BACK_SENEOR)){
                if(!getInput(IN1_1_SWITCH) && !runtimedata.Virtual_Btn[IN1_1_SWITCH])
                {
                    setOutput(OUT0_SOL1_FRONT, 0);
                    setOutput(OUT1_SOL1_BACK, 0);
                    runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
                }
            }
            break;
        case 130:
            Counter_count();
            runtimedata.system_status = Normal_Count;
            runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            break;
        case 140:
            if(!getInput(IN1_1_SWITCH) && !runtimedata.Virtual_Btn[IN1_1_SWITCH]){
                runtimedata.Workindex[WORKINDEX_NORMAL] = 0;
                buzzerPlay(200);
            }
            break;
    }
}
#endif

void NormalProcess()
{
    if(runtimedata.preWorkindex[WORKINDEX_NORMAL] != runtimedata.Workindex[WORKINDEX_NORMAL])
    {
        runtimedata.preWorkindex[WORKINDEX_NORMAL] = runtimedata.Workindex[WORKINDEX_NORMAL];
        DEBUG("WORKINDEX_NORMAL: " + String(runtimedata.preWorkindex[WORKINDEX_NORMAL]));
    }
    switch(runtimedata.Workindex[WORKINDEX_NORMAL])
    {
        case 0:
            Device_Stop();
            for(int i=0; i<3; i++)
                runtimedata.Sensor_345_State[i] = 0;
            if(!getInput(IN1_1_SWITCH))
            {
                runtimedata.Workindex[WORKINDEX_NORMAL] += 20;
                runtimedata.system_status = Normal_Free;
            }
            break;
        /*按下按鈕1，再放開按鈕1後-->1汽缸前進-->1汽缸到位-->2汽缸前進-->2汽缸到位*/
        case 20://按鈕1按下
            if(getInput(IN1_1_SWITCH))
            {
                runtimedata.system_status = Normal_Btn_1;
                runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            }
            break;
        case 30://放開按鈕1後-->1汽缸前進
            if (!getInput(IN1_1_SWITCH))
            {
                setOutput(OUT1_SOL1_BACK, 0);
                setOutput(OUT0_SOL1_FRONT, 1);
                runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            }
            break;
        case 40://等待1汽缸到位-->2汽缸前進
            if(getInput(IN6_1_FRONT_SENEOR) && !getInput(IN7_1_BACK_SENEOR))
            {
                setOutput(OUT2_SOL2_FRONT, 1);
                setOutput(OUT3_SOL2_BACK, 0);
                runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            }
            break;
        case 50://等待2汽缸到位
            if(getInput(IN8_2_FRONT_SENEOR) && !getInput(IN9_2_BACK_SENEOR))
            {
                runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            }
            break;
        //再按按鈕1，再放開按鈕1後-->2汽缸後退-->2汽缸到位-->1汽缸後退-->1汽缸到位-->計數+1
        case 60://按鈕1按下
            if(getInput(IN1_1_SWITCH))
            {
                runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            }
            break;
        case 70://放開按鈕1後-->2汽缸後退
            if(!getInput(IN1_1_SWITCH))
            {
                setOutput(OUT2_SOL2_FRONT, 0);
                setOutput(OUT3_SOL2_BACK, 1);
                runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            }
            break;
        case 80://等2汽缸到位-->1汽缸後退
            if(!getInput(IN8_2_FRONT_SENEOR) && getInput(IN9_2_BACK_SENEOR))
            {
                setOutput(OUT1_SOL1_BACK, 1);
                setOutput(OUT0_SOL1_FRONT, 0);
                runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            }
            break;
        case 90://等到1汽缸到位
            if(!getInput(IN6_1_FRONT_SENEOR) && getInput(IN7_1_BACK_SENEOR))
            {
                runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            }
            break;
        case 100:
            Counter_count();
            runtimedata.system_status = Normal_Count;
            runtimedata.Workindex[WORKINDEX_NORMAL] += 10;
            break;
        case 110:
            if(!getInput(IN1_1_SWITCH))
            {
                runtimedata.Workindex[WORKINDEX_NORMAL] = 0;
                buzzerPlay(200);
            }
            break;
    }
}

void Btn345_Process()
{
/*  
    20211116
    按鈕3、按鈕4、按鈕5:()
    按著前進，放開後退(任何時候，手動跟指令都要可使用)
    若"按鈕3、按鈕4、按鈕5" 手動跟指令需切換，
    就需要重啟、或是按急停再解除，才能進行切換。
*/
    if(getInput(IN3_3_SWITCH) || runtimedata.Virtual_Btn[IN3_3_SWITCH]){
        setOutput(OUT5_SOL3_BACK, 0);
        setOutput(OUT4_SOL3_FRONT, 1);
        runtimedata.system_status = Normal_Btn_3;
    }
    else{
        setOutput(OUT4_SOL3_FRONT, 0);
        setOutput(OUT5_SOL3_BACK, 1);
        runtimedata.system_status = Normal_Btn_3;
    }
    //-----------------------------------
    if(getInput(IN4_4_SWITCH) || runtimedata.Virtual_Btn[IN4_4_SWITCH]){
        setOutput(OUT7_SOL4_BACK, 0);
        setOutput(OUT6_SOL4_FRONT, 1);
        runtimedata.system_status = Normal_Btn_4;
    }
    else{
        setOutput(OUT6_SOL4_FRONT, 0);
        setOutput(OUT7_SOL4_BACK, 1);
        runtimedata.system_status = Normal_Btn_4;
    }
    //-----------------------------------
    if(getInput(IN5_5_SWITCH) || runtimedata.Virtual_Btn[IN5_5_SWITCH]){
        setOutput(OUT9_SOL5_BACK, 0);
        setOutput(OUT8_SOL5_FRONT, 1);
        runtimedata.system_status = Normal_Btn_5;
    }
    else{
        setOutput(OUT8_SOL5_FRONT, 0);
        setOutput(OUT9_SOL5_BACK, 1);
        runtimedata.system_status = Normal_Btn_5;
    }
}

bool Device_Init() //所有設備依序回到初始狀態
{
    bool isfinish = false;
    if(runtimedata.preWorkindex[WORKINDEX_INIT] != runtimedata.Workindex[WORKINDEX_INIT])
    {
        runtimedata.preWorkindex[WORKINDEX_INIT] = runtimedata.Workindex[WORKINDEX_INIT];
        DEBUG("WORKINDEX_INIT: " + String(runtimedata.preWorkindex[WORKINDEX_INIT]));
    }
    switch(runtimedata.Workindex[WORKINDEX_INIT])
    {
        case 0:
            Device_Stop();
            runtimedata.Workindex[WORKINDEX_INIT] += 10;
            break;
        case 10:
            setOutput(OUT9_SOL5_BACK, 1);
            if(getInput(IN12_5_SENEOR))
            {
                setOutput(OUT9_SOL5_BACK, 0);
                runtimedata.Workindex[WORKINDEX_INIT] += 10;
            }
            break;
        case 20:
            setOutput(OUT7_SOL4_BACK, 1);
            if(getInput(IN11_4_SENEOR))
            {
                setOutput(OUT7_SOL4_BACK, 0);
                runtimedata.Workindex[WORKINDEX_INIT] += 10;
            }
            break;
        case 30:
            setOutput(OUT5_SOL3_BACK, 1);
            if(getInput(IN10_3_SENEOR))
            {
                setOutput(OUT5_SOL3_BACK, 0);
                runtimedata.Workindex[WORKINDEX_INIT] += 10;
            }
            break;
        case 40:
            setOutput(OUT3_SOL2_BACK, 1);
            if(getInput(IN9_2_BACK_SENEOR))
            {
                setOutput(OUT3_SOL2_BACK, 0);
                runtimedata.Workindex[WORKINDEX_INIT] += 10;
            }
            break;
        case 50:
            setOutput(OUT1_SOL1_BACK, 1);
            if(getInput(IN7_1_BACK_SENEOR))
            {
                setOutput(OUT1_SOL1_BACK, 0);
                runtimedata.Workindex[WORKINDEX_INIT] += 10;
            }
            break;
        default:
            isfinish = true;
            break;
    }
    return isfinish;
}

void Device_Stop() //所有設備停止移動
{
    DEBUG("Device_Stop");
    for(int i=0; i<10; i++)
    {
        setOutput(i, 0);
    }
    for(int i=0; i<6; i++)
        runtimedata.Virtual_Btn[i] = false;
}
void Counter_reset()
{
    setOutput(OUT11_COUNTER_RESET, 1);
    delay(100);
    setOutput(OUT11_COUNTER_RESET, 0);
    runtimedata.Conut = 0;
    DEBUG("Conut: " + String(runtimedata.Conut));
}
void Counter_count()
{
    setOutput(OUT10_COUNTER_COUNT, 1);
    delay(100);
    setOutput(OUT10_COUNTER_COUNT, 0);
    runtimedata.Conut ++;
    DEBUG("Conut: " + String(runtimedata.Conut));
}

void buzzerPlay(int playMS)
{
  digitalWrite(BUZZ, HIGH);
  delay(playMS);
  digitalWrite(BUZZ, LOW);
}


