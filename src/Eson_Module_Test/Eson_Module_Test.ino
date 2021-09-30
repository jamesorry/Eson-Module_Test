#include "UserCommand.h"
#include "EEPROM_Function.h"
#include "MainProcess.h"
#include "hmi.h"
#include "Timer.h"
#include "ControlCommand.h"

HardwareSerial *cmd_port;
HardwareSerial *Ctrl_cmd_port;

extern MainDataStruct maindata;
extern RuntimeStatus runtimedata;

void setup() {
	cmd_port = &CMD_PORT;
	cmd_port->begin(CMD_PORT_BR);
    Ctrl_cmd_port = &CONTORL_CMD_PORT;
    Ctrl_cmd_port->begin(CONTORL_CMD_PORT_BR);
	READ_EEPROM();
	MainProcess_Init();
    TimerInit(1, 10000);
	buzzerPlay(1000);
//    cmd_port->println("VERSTR: " + String(VERSTR));
    DEBUG("VERSTR: " + String(VERSTR));
}

void loop() {
	UserCommand_Task();
	MainProcess_Task();
    Ctrl_UserCommand_Task();
	if(runtimedata.UpdateEEPROM)
	{
		runtimedata.UpdateEEPROM = false;
		WRITE_EEPROM(); //maindata內的值都會寫到EEPROM
	}
}

ISR(TIMER1_COMPA_vect)
{
    if(runtimedata.RunMode != RUN_MODE_EMERGENCY_STOP && (!digitalRead(InputPin[IN0_EMERGENCY]) || !runtimedata.Virtual_Emergency))
    {
        runtimedata.RunMode = RUN_MODE_EMERGENCY_STOP;
    }
}

