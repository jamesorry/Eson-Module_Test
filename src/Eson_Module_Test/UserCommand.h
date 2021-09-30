#ifndef _USER_COMMAND_H_
#define	_USER_COMMAND_H_

#include "Arduino.h"

typedef struct __CMD {
  const char* cmd;
  void (*func)(void);
} CMD, *PCMD;

void resetArduino(void);
void getAdc(void);
void getGpio(void);
void setGpio(void);
void echoOn(void);
void echoOff(void);
void cmd_CodeVer(void);
void showHelp(void);
bool getNextArg(String &arg);
void cmdOutput(void);
void cmdInput(void);
void cmdGetSetADC();
void cmd_SaveEEPROM();
void cmd_ClearEEPROM();
void cmd_ReadEEPROM();
void cmdRunMode(void);
void cmd_Counter();
void cmd_sys_status();
void cmd_Btn_On_Off();
void cmd_Emergency();
void cmd_Virtual_Btn();

void UserCommand_Task(void);
void UserCommand_Timer(void);
#endif //_USER_COMMAND_H_
