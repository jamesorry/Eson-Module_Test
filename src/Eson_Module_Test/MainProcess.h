#ifndef _MAIN_PROCESS_H_
#define _MAIN_PROCESS_H_

#include "Arduino.h"

#define	EXTIO_NUM			4 //8個IO為一組
#define	INPUT_8_NUMBER		2
#define OUTPUT_8_NUMBER		1

#define	OUTPUT_NONE_ACTIVE	0
#define	OUTPUT_ACTIVE		1

#define	INPUT_NONE_ACTIVE	0
#define	INPUT_ACTIVE		1

#define WORKINDEX_TOTAL 	4
#define WORKINDEX_INIT      1
#define WORKINDEX_NORMAL    2

#define BUZZ				48

#define IN0_EMERGENCY           0
#define IN1_1_SWITCH            1
#define IN2_2_SWITCH            2
#define IN3_3_SWITCH            3
#define IN4_4_SWITCH            4
#define IN5_5_SWITCH            5
#define IN6_1_FRONT_SENEOR      6
#define IN7_1_BACK_SENEOR       7
#define IN8_2_FRONT_SENEOR      8
#define IN9_2_BACK_SENEOR       9
#define IN10_3_SENEOR           10
#define IN11_4_SENEOR           11
#define IN12_5_SENEOR           12

//SOL ==> 電磁閥
#define OUT0_SOL1_FRONT         0
#define OUT1_SOL1_BACK          1
#define OUT2_SOL2_FRONT         2
#define OUT3_SOL2_BACK          3
#define OUT4_SOL3_FRONT         4
#define OUT5_SOL3_BACK          5
#define OUT6_SOL4_FRONT         6
#define OUT7_SOL4_BACK          7
#define OUT8_SOL5_FRONT         8
#define OUT9_SOL5_BACK          9
#define OUT10_COUNTER_COUNT     10
#define OUT11_COUNTER_RESET     11

#define RUN_MODE_EMERGENCY_STOP		0
#define RUN_MODE_INIT				1
#define RUN_MODE_NORMAL             2


typedef struct _DigitalIO_
{
	uint8_t	Input[4];
	uint8_t	Output[4];
	uint8_t PreOutput[4];
}DigitalIO;

typedef struct _MainDataStruct_
{
//	此處的變數值會寫到EEPROM
	char 		Vendor[10];
	uint8_t 	HMI_ID;
}MainDataStruct;


typedef struct _RuntimeStruct_
{
//	此處為啟動後才會使用的變數
	int  	Workindex[WORKINDEX_TOTAL] = {0};
	int		preWorkindex[WORKINDEX_TOTAL] = {-1};
	
	uint8_t sensor[INPUT_8_NUMBER*8 + EXTIO_NUM*8];
	uint8_t outbuf[(OUTPUT_8_NUMBER+EXTIO_NUM)*8];

	bool 		UpdateEEPROM;
    int         RunMode;
    int         preRunMode = -1;
    uint32_t    Conut = 0;
    bool        Sensor_345_State[3] = {0};
    int         system_status;
    bool        Virtual_Btn[6] = {0};
    bool        Virtual_Emergency = true;
}RuntimeStatus;

enum SystemStatus{
    Emergency_Stop,
    Init,
    Normal_Free,
    Normal_Btn_1,
    Normal_Btn_2,
    Normal_Btn_3,
    Normal_Btn_4,
    Normal_Btn_5,
    Normal_Back_Btn_2,
    Normal_Back_Btn_1,
    Normal_Count
};
void setOutput(uint8_t index, uint8_t hl);
uint8_t getInput(uint8_t index);


void MainProcess_Task();
void MainProcess_Init();
bool Device_Init();
void Device_Stop();
void NormalProcess();
void Counter_reset();
void Counter_count();
void buzzerPlay(int playMS);

#endif	//_MAIN_PROCESS_H_

