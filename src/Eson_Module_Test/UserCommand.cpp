#include <avr/wdt.h>
#include "SoftwareSerial.h"
#include "UserCommand.h"
#include "EEPROM_Function.h"
#include "MainProcess.h"
#include "hmi.h"

#define USER_COMMAND_DEBUG  1
extern HardwareSerial *cmd_port;
extern MainDataStruct maindata;
extern RuntimeStatus runtimedata;
extern DigitalIO digitalio;

CMD g_cmdFunc[] = {
//在這新增function name 以及所呼叫的function
	{"adc", getAdc},
	{"getgpio", getGpio},
	{"setgpio", setGpio},
	{"reset", resetArduino},
	{"echoon", echoOn},
	{"echooff", echoOff},
    {"GetSetADC", cmdGetSetADC},
	{"version", cmd_CodeVer},
	{"out", cmdOutput},
	{"in", cmdInput},
    {"SD", cmd_SaveEEPROM},
    {"CD", cmd_ClearEEPROM},
    {"RD", cmd_ReadEEPROM},
    {"RunMode", cmdRunMode},
    {"counter", cmd_Counter},
    {"status", cmd_sys_status},
    {"SBT", cmd_Btn_On_Off}, //測試按鈕 SBT 1 ON / SBT 1 OFF
    {"SBE", cmd_Emergency}, //緊急按鈕 SBE ON / SBE OFF
    {"virtual_Btn", cmd_Virtual_Btn},
	{"?", showHelp}
};


String g_inputBuffer0 = "";
String* g_inputBuffer = NULL;
String g_cmd = "";
String g_arg = "";

bool g_echoOn = true;
uint32_t targetcnt = 0;
bool getNextArg(String &arg)
{
	if (g_arg.length() == 0)
		return false;
	if (g_arg.indexOf(" ") == -1)
	{
		arg = g_arg;
		g_arg.remove(0);
	}
	else
	{
		arg = g_arg.substring(0, g_arg.indexOf(" "));
		g_arg = g_arg.substring(g_arg.indexOf(" ") + 1);
	}
	return true;
}

void resetArduino(void)
{
	wdt_enable(WDTO_500MS);
	while (1);
}
void showHelp(void)
{
	int i;

	cmd_port->println("");
	for (i = 0; i < (sizeof(g_cmdFunc) / sizeof(CMD)); i++)
	{
		cmd_port->println(g_cmdFunc[i].cmd);
	}
}

void getAdc(void)
{
	String arg1;
	int analogPin;
	int value;

	if (!getNextArg(arg1))
	{
		cmd_port->println("No parameter");
		return;
	}
	analogPin = arg1.toInt();
	value = analogRead(analogPin);
	cmd_port->print("ADC_");
	cmd_port->print(analogPin);
	cmd_port->print(" : ");
	cmd_port->println(value);
}

void getGpio(void)
{
  String arg1, arg2;
  int digitalPin, pullUp;
  int value;

  if (!getNextArg(arg1))
  {
    cmd_port->println("No parameter");
    return;
  }
  if (!getNextArg(arg2))
  {
    pullUp = 0;
  }
  else
  {
    pullUp = arg2.toInt();
  }
  digitalPin = arg1.toInt();
  if (arg2.compareTo("na") == 0)
  {
    cmd_port->println("pin mode keep original");
  }
  else
  {
    if (pullUp)
    {
      cmd_port->println("pull-up");
      pinMode(digitalPin, INPUT_PULLUP);
    }
    else
    {
      cmd_port->println("no-pull");
      pinMode(digitalPin, INPUT);
    }
  }

  cmd_port->print("GPIO:");
  cmd_port->println(arg1);

  value = digitalRead(digitalPin);

  cmd_port->print("input value:");
  cmd_port->println(value);
}

void setGpio(void)
{
  String arg1, arg2;
  int digitalPin;
  int value;

  if (!getNextArg(arg1))
  {
    cmd_port->println("No parameter 1");
    return;
  }
  if (!getNextArg(arg2))
  {
    cmd_port->println("No parameter 2");
    return;
  }
  digitalPin = arg1.toInt();
  value = arg2.toInt();

  cmd_port->print("GPIO:");
  cmd_port->println(arg1);
  cmd_port->print("level:");
  cmd_port->println(arg2);

  digitalWrite(digitalPin, value ? HIGH : LOW);
  pinMode(digitalPin, OUTPUT);
}

void echoOn(void)
{
  g_echoOn = true;
}

void echoOff(void)
{
  g_echoOn = false;
}

void cmd_CodeVer(void)
{
  cmd_port->println(VERSTR);
}

void cmdOutput(void)
{
	String arg1, arg2;
	int digitalPin;
	int value;

	if (!getNextArg(arg1))
	{
		cmd_port->println("No parameter 1");
		return;
	}
	if (!getNextArg(arg2))
	{
		cmd_port->println("No parameter 2");
		return;
	}
	digitalPin = arg1.toInt();
	value = arg2.toInt();

	cmd_port->print("PIN index:");
	cmd_port->println(arg1);
	cmd_port->print("level:");
	cmd_port->println(arg2);

	setOutput((uint8_t)digitalPin, (uint8_t)(value ? HIGH : LOW));
	cmd_port->println("");
}

void cmdInput(void)
{
	String arg1;
	unsigned long pinindex;

	getNextArg(arg1);
	if( (arg1.length()==0))
	{
		cmd_port->println("Please input enough parameters");
		return;
	}
	pinindex = arg1.toInt();
	cmd_port->println("Sensor: " + String(getInput(pinindex)));
}

void cmdGetSetADC()
{
	String arg1, arg2;
	int Pin,value;
	if(!getNextArg(arg1))
	{
	  cmd_port->println("No parameter 1");
	  return;
	}
    Pin = arg1.toInt();
	if(!getNextArg(arg2))
	{
	    cmd_port->println("No parameter 2");
        value = analogRead(ADC_PWMPin[Pin]);
        cmd_port->println("Pin " + String(Pin) + ": " + String(value));
        return;
	}
	value = arg2.toInt();
    if(Pin%2 == 0){
        pinMode(ADC_PWMPin[Pin], OUTPUT);
        analogWrite(ADC_PWMPin[Pin], value);
        cmd_port->println("PWM value: " + String(Pin) + String(value));
    }
    else
    {
        pinMode(ADC_PWMPin[Pin], OUTPUT);
        analogWrite(ADC_PWMPin[Pin], value);
        cmd_port->println("ADC value: " + String(Pin) + String(value));
    }
}

void cmd_SaveEEPROM()
{
    runtimedata.UpdateEEPROM = true;
}
void cmd_ClearEEPROM()
{
    Clear_EEPROM();
}
void cmd_ReadEEPROM()
{
    READ_EEPROM();
}

void cmdRunMode(void)
{
	String arg1, arg2;
	int runmode = 0;
	
	if(getNextArg(arg1))
	{
		runtimedata.RunMode = arg1.toInt();
		runtimedata.Workindex[runtimedata.RunMode] = 0;
	}
	cmd_port->println("Run mode: " + String(runtimedata.RunMode));
}

void cmd_Counter()
{
	String arg1;
	if(getNextArg(arg1))
	{
		if(arg1 == "reset")
            Counter_reset();
	}
    else
    {
        Counter_count();
    }
}

char *SysStatusStrings[] = 
{ 
    "Emergency_Stop: 0",
              "Init: 1",
       "Normal_Free: 2",
      "Normal_Btn_1: 3",
      "Normal_Btn_2: 4",
      "Normal_Btn_3: 5",
      "Normal_Btn_4: 6",
      "Normal_Btn_5: 7",
 "Normal_Back_Btn_2: 8",
 "Normal_Back_Btn_1: 9",
      "Normal_Count: 10"
};
const size_t nb_strings = sizeof(SysStatusStrings) / sizeof(SysStatusStrings[0]);

void cmd_sys_status()
{
    for(int i=0; i<nb_strings; i++){
        cmd_port->println(SysStatusStrings[i]);
    }
    DEBUG("Now status: " + String(runtimedata.system_status));
}

void cmd_Btn_On_Off()
{
    String arg1, arg2;
    int num = 0;
    bool state = false;
    if(!getNextArg(arg1)){
        return;
    }
    num = arg1.toInt();
    if(num > 5 || num< 1) return;
    if(!getNextArg(arg2)){
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
}
void cmd_Emergency()
{
    String arg1;
    bool state = false;
    if(!getNextArg(arg1)){
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
}

void cmd_Virtual_Btn()
{
    for(int k=1; k<6; k++){
        cmd_port->print("SBT ");
        cmd_port->print(k);
        cmd_port->print(": ");
        if(runtimedata.Virtual_Btn[k] == 0)
            cmd_port->println("OFF");
        else if(runtimedata.Virtual_Btn[k] == 1)
            cmd_port->println("ON");
    }
    
    cmd_port->print("SBE ");
    if(runtimedata.Virtual_Emergency == 0)
        cmd_port->println("OFF");
    else if(runtimedata.Virtual_Emergency == 1)
        cmd_port->println("ON");
}
uint8_t UserCommWorkindex = 0;
uint32_t UserCommandTimeCnt = 0;
void UserCommand_Task(void)
{
  int i, incomingBytes, ret, cmdPortIndex;
  char data[2] = {0};
  switch(UserCommWorkindex)
  {
    case 0:
    {
      if(cmd_port->available())
      {
        g_inputBuffer = &g_inputBuffer0;
        UserCommWorkindex ++;
        UserCommandTimeCnt = millis();
      }
      break;
    }
    case 1:
    {
      if((millis() - UserCommandTimeCnt) > 50)
        UserCommWorkindex ++;
      break;
    }
    case 2:
    {
      if ( incomingBytes = cmd_port->available() )
      {
#if USER_COMMAND_DEBUG
      cmd_port->println("cmd_port datalen: " + String(incomingBytes));
#endif
      for ( i = 0; i < incomingBytes; i++ )
      {
        ret = cmd_port->read();
        if ( (ret >= 0x20) && (ret <= 0x7E) || (ret == 0x0D) || (ret == 0x0A) )
        {
        data[0] = (char)ret;
        (*g_inputBuffer) += data;
        if (g_echoOn)
        {
          if ( (data[0] != 0x0D) && (data[0] != 0x0A) ){
#if USER_COMMAND_DEBUG
          cmd_port->write(data);
#endif
            }
        }
        }
        else if (ret == 0x08)
        {
        if (g_inputBuffer->length())
        {
          g_inputBuffer->remove(g_inputBuffer->length() - 1);
          if (g_echoOn)
          {
          data[0] = 0x08;
#if USER_COMMAND_DEBUG
          cmd_port->write(data);
#endif
          }
        }
        }
      }
      if (g_inputBuffer->indexOf('\r') == -1)
      {
        if (g_inputBuffer->indexOf('\n') == -1)
        return;
      }
      g_inputBuffer->trim();
      while (g_inputBuffer->indexOf('\r') != -1)
        g_inputBuffer->remove(g_inputBuffer->indexOf('\r'), 1);
      while (g_inputBuffer->indexOf('\n') != -1)
        g_inputBuffer->remove(g_inputBuffer->indexOf('\n'), 1);
      while (g_inputBuffer->indexOf("  ") != -1)
        g_inputBuffer->remove(g_inputBuffer->indexOf("  "), 1);
    
#if USER_COMMAND_DEBUG
      cmd_port->println();
#endif
    
      if (g_inputBuffer->length())
      {
        g_arg.remove(0);
        if (g_inputBuffer->indexOf(" ") == -1)
        g_cmd = (*g_inputBuffer);
        else
        {
        g_cmd = g_inputBuffer->substring(0, g_inputBuffer->indexOf(" "));
        g_arg = g_inputBuffer->substring(g_inputBuffer->indexOf(" ") + 1);
        }
        for (i = 0; i < (sizeof(g_cmdFunc) / sizeof(CMD)); i++)
        {
        //if(g_cmd==g_cmdFunc[i].cmd)
        if (g_cmd.equalsIgnoreCase(g_cmdFunc[i].cmd))
        {
          g_cmdFunc[i].func();
#if USER_COMMAND_DEBUG
          cmd_port->print("ARDUINO>");
#endif
          break;
        }
        else if (i == (sizeof(g_cmdFunc) / sizeof(CMD) - 1))
        {
#if USER_COMMAND_DEBUG
          cmd_port->println("bad command !!");
          cmd_port->print("ARDUINO>");
#endif
        }
        }
        *g_inputBuffer = "";
      }
      else
      {
#if USER_COMMAND_DEBUG
        cmd_port->print("ARDUINO>");
#endif
      }
      UserCommWorkindex = 0;
      break;
    }
  }

  }
}

