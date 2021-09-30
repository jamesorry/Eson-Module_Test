#include <avr/wdt.h>
#include "MainProcess.h"
#include "Timer.h"
#include "ControlCommand.h"
#include "hmi.h"

#define CTRL_DEBUG 0
extern HardwareSerial *cmd_port;
extern HardwareSerial *Ctrl_cmd_port;
extern RuntimeStatus	runtimedata;
extern MainDataStruct maindata;
extern DigitalIO digitalio;

HRMCMD Ctrl_g_cmdFunc[] = {
    {"version", fw_version}, //確認FW版本
    {"SBT", fw_Btn_On_Off}, //測試按鈕 SBT 1 ON / SBT 1 OFF
    {"SBE", fw_Emergency}, //緊急按鈕 SBE ON / SBE OFF
    {"SB_Status", fw_Virtual_Btn_status},
    {"Reset_CNT", fw_Counter},
    {"SOL", fw_output},// 控制電磁閥前進後退
    {"Init", fw_Init},
    {"?", CtrlshowHelp}
};
void fw_Btn_On_Off()
{
    String arg1, arg2;
    int num = 0;
    bool state = false;
    if(!CtrlgetNextArg(arg1)){
        return;
    }
    num = arg1.toInt();
    if(num > 5 || num< 1) return;
    if(!CtrlgetNextArg(arg2)){
        return;
    }
    if(arg2 == "ON"){
        state = true;
    }
    else if(arg2 == "OFF"){
        state = false;
    }
    else return;
    if(runtimedata.RunMode == RUN_MODE_NORMAL){
        runtimedata.Virtual_Btn[num] = state;
    }
    Ctrl_cmd_port->println("SBT " + arg1 + " " + arg2);
}
void fw_Emergency()
{
    String arg1;
    bool state = false;
    if(!CtrlgetNextArg(arg1)){
        return;
    }
    //緊急開關為B接點
    if(arg1 == "ON"){
        state = false;
    }
    else if(arg1 == "OFF"){
        state = true;
    }
    else return;
    runtimedata.Virtual_Emergency = state;
    
    Ctrl_cmd_port->println("SBE " + arg1);
}

void fw_Virtual_Btn_status()
{
    for(int k=1; k<6; k++){
        Ctrl_cmd_port->print("SBT ");
        Ctrl_cmd_port->print(k);
        Ctrl_cmd_port->print(": ");
        if(runtimedata.Virtual_Btn[k] == 0)
            Ctrl_cmd_port->println("OFF");
        else if(runtimedata.Virtual_Btn[k] == 1)
            Ctrl_cmd_port->println("ON");
    }
    Ctrl_cmd_port->print("SBE ");
    if(runtimedata.Virtual_Emergency == 0)
        Ctrl_cmd_port->println("OFF");
    else if(runtimedata.Virtual_Emergency == 1)
        Ctrl_cmd_port->println("ON");
}

void fw_Counter()
{
    Counter_reset();
    Ctrl_cmd_port->println("Reset_CNT");
}

void fw_version()//確認FW版本
{
    Ctrl_cmd_port->println(VERSTR);
}
void fw_output()
{
    String arg1, arg2;
    int num = 0;
    bool state = false;
    if(!CtrlgetNextArg(arg1)){
        return;
    }
    num = arg1.toInt();
    if(num > 5 || num< 1) return;
    if(!CtrlgetNextArg(arg2)){
        return;
    }
    //forward, backward
    if(runtimedata.Workindex[WORKINDEX_NORMAL] == 10){
        if(arg2 == "forward"){
            setOutput(num*2-2, 1);
            setOutput(num*2-1, 0);
        }
        else if(arg2 == "backward"){
            setOutput(num*2-2, 0);
            setOutput(num*2-1, 1);
        }
        else return;
    }
    else return;
    Ctrl_cmd_port->println("SOL " + arg1 + " " + arg2);
}

void fw_Init()
{
    runtimedata.RunMode = RUN_MODE_EMERGENCY_STOP;
    Ctrl_cmd_port->println("Init");
}

String Ctrl_g_inputBuffer0 = "";
String* Ctrl_g_inputBuffer = NULL;
String Ctrl_g_cmd = "";
String Ctrl_g_arg = "";

bool Ctrl_g_echoOn = true;

bool CtrlgetNextArg(String &arg)
{
  	if (Ctrl_g_arg.length() == 0)
    	return false;
  	if (Ctrl_g_arg.indexOf(" ") == -1)
  	{
    	arg = Ctrl_g_arg;
    	Ctrl_g_arg.remove(0);
  	}
  	else
  	{
    	arg = Ctrl_g_arg.substring(0, Ctrl_g_arg.indexOf(" "));
    	Ctrl_g_arg = Ctrl_g_arg.substring(Ctrl_g_arg.indexOf(" ") + 1);
  	}
  	return true;
}

void CtrlshowHelp(void)
{
	int i;

	Ctrl_cmd_port->println("");
	for (i = 0; i < (sizeof(Ctrl_g_cmdFunc) / sizeof(HRMCMD)); i++)
	{
		Ctrl_cmd_port->println(Ctrl_g_cmdFunc[i].cmd);
	}
}


uint8_t Ctrl_UserCommWorkindex = 0;
uint32_t Ctrl_UserCommandTimeCnt = 0;

void Ctrl_UserCommand_Task(void)
{
  	int i, incomingBytes, ret, cmdPortIndex;
 	char data[2] = {0};

	switch(Ctrl_UserCommWorkindex)
	{
		case 0:
		{
			if(Ctrl_cmd_port->available())
			{
				Ctrl_g_inputBuffer = &Ctrl_g_inputBuffer0;
				Ctrl_UserCommWorkindex ++;
				Ctrl_UserCommandTimeCnt = millis();
			}
			break;
		}
		case 1:
		{
			if((millis() - Ctrl_UserCommandTimeCnt) > 50)
				Ctrl_UserCommWorkindex ++;
			break;
		}
		case 2:
		{
		  	if ( incomingBytes = Ctrl_cmd_port->available() )
		  	{
#if CTRL_DEBUG
				Ctrl_cmd_port->println("Ctrl_cmd_port datalen: " + String(incomingBytes));
#endif
				for ( i = 0; i < incomingBytes; i++ )
				{
			  		ret = Ctrl_cmd_port->read();
			  		if ( (ret >= 0x20) && (ret <= 0x7E) || (ret == 0x0D) || (ret == 0x0A) )
			  		{
						data[0] = (char)ret;
						(*Ctrl_g_inputBuffer) += data;
						if (Ctrl_g_echoOn)
						{
				  			if ( (data[0] != 0x0D) && (data[0] != 0x0A) )
							{
#if CTRL_DEBUG
							Ctrl_cmd_port->write(data);
#endif
				  			}
						}
			  		}
			  		else if (ret == 0x08)
			  		{
						if (Ctrl_g_inputBuffer->length())
						{
				  			Ctrl_g_inputBuffer->remove(Ctrl_g_inputBuffer->length() - 1);
				  			if (Ctrl_g_echoOn)
				  			{
								data[0] = 0x08;
								Ctrl_cmd_port->write(data);
				  			}
						}
			  		}
				}
				if (Ctrl_g_inputBuffer->indexOf('\r') == -1)
				{
			  		if (Ctrl_g_inputBuffer->indexOf('\n') == -1)
						return;
				}
				Ctrl_g_inputBuffer->trim();
				while (Ctrl_g_inputBuffer->indexOf('\r') != -1)
			  		Ctrl_g_inputBuffer->remove(Ctrl_g_inputBuffer->indexOf('\r'), 1);
				while (Ctrl_g_inputBuffer->indexOf('\n') != -1)
			  		Ctrl_g_inputBuffer->remove(Ctrl_g_inputBuffer->indexOf('\n'), 1);
				while (Ctrl_g_inputBuffer->indexOf("  ") != -1)
			  		Ctrl_g_inputBuffer->remove(Ctrl_g_inputBuffer->indexOf("  "), 1);
#if CTRL_DEBUG
				Ctrl_cmd_port->println();
#endif
				if (Ctrl_g_inputBuffer->length())
				{
			  		Ctrl_g_arg.remove(0);
			  		if (Ctrl_g_inputBuffer->indexOf(" ") == -1)
						Ctrl_g_cmd = (*Ctrl_g_inputBuffer);
			  		else
			  		{
						Ctrl_g_cmd = Ctrl_g_inputBuffer->substring(0, Ctrl_g_inputBuffer->indexOf(" "));
						Ctrl_g_arg = Ctrl_g_inputBuffer->substring(Ctrl_g_inputBuffer->indexOf(" ") + 1);
			  		}
			  		for (i = 0; i < (sizeof(Ctrl_g_cmdFunc) / sizeof(HRMCMD)); i++)
			  		{
						if (Ctrl_g_cmd.equalsIgnoreCase(Ctrl_g_cmdFunc[i].cmd))
						{
				  			Ctrl_g_cmdFunc[i].func();
#if CTRL_DEBUG
				  			Ctrl_cmd_port->print("Ctrl>");
#endif
				  			break;
						}
						else if (i == (sizeof(Ctrl_g_cmdFunc) / sizeof(HRMCMD) - 1))
						{
#if CTRL_DEBUG
							Ctrl_cmd_port->println("bad command !!");
				  			Ctrl_cmd_port->print("Ctrl>");
#endif
						}
			  		}
			  		*Ctrl_g_inputBuffer = "";
				}
				else
				{
#if CTRL_DEBUG
			  		Ctrl_cmd_port->print("Ctrl>");
#endif
				}
				Ctrl_UserCommWorkindex = 0;
				break;
			}
		}
  	}
}


