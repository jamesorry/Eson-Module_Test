#ifndef _HRM_COMMAND_H_
#define	_HRM_COMMAND_H_

#include "Arduino.h"

typedef struct __HRMCMD {
  const char* cmd;
  void (*func)(void);
} HRMCMD, *HRMPCMD;

void Ctrl_UserCommand_Task(void);
void CtrlshowHelp(void);
bool CtrlgetNextArg(String &arg);

void fw_version();
void fw_Btn_On_Off();
void fw_Emergency();
void fw_Counter();
void fw_Virtual_Btn_status();
void fw_output();
void fw_Init();

#endif //_HRM_COMMAND_H_

