using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace RegistryLib
{
    public class RegistryProcessor
    {


        public enum RegistryParts
        { 
            HKEY_CLASSES_ROOT,
            HKEY_CURRENT_USER,
            HKEY_LOCAL_MACHINE,
            HKEY_USERS,
            HKEY_CURRENT_CONFIG
        }


        public static State.ErrorCode SetToRegistry(string regPath, string regName, string regValue, RegistryProcessor.RegistryParts regPart)
        {
            if (regPath == null || regPath.Length <= 0 || regName == null || regName.Length <= 0 || regValue == null || regValue.Length <= 0)
                return State.ErrorCode.VAL_NOT_DEF;
            try
            {
                Microsoft.Win32.RegistryKey key = null;
                switch (regPart)
                {
                    case RegistryParts.HKEY_CLASSES_ROOT:
                        key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(regPath);
                        break;
                    case RegistryParts.HKEY_CURRENT_CONFIG:
                        key = Microsoft.Win32.Registry.CurrentConfig.CreateSubKey(regPath);
                        break;
                    case RegistryParts.HKEY_CURRENT_USER:
                        key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(regPath);
                        break;
                    case RegistryParts.HKEY_LOCAL_MACHINE:
                        key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(regPath);
                        break;
                    case RegistryParts.HKEY_USERS:
                        key = Microsoft.Win32.Registry.Users.CreateSubKey(regPath);
                        break;
                }
                key.SetValue(regName, regValue);
                key.Close();
                return State.ErrorCode.ALL_RIGTH;
            }
            catch
            {
                return State.ErrorCode.FATAL_REG_ERROR;
            }
        }


        public static State.ErrorCode GetFromRegistry(string regPath, string regName, ref string regValue, RegistryProcessor.RegistryParts regPart)
        {
            if (regPath == null || regPath.Length <= 0 || regName == null || regName.Length <= 0)
                return State.ErrorCode.VAL_NOT_DEF;
            try
            {
                Microsoft.Win32.RegistryKey key = null;
                switch (regPart)
                {
                    case RegistryParts.HKEY_CLASSES_ROOT:
                        key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(regPath);
                        break;
                    case RegistryParts.HKEY_CURRENT_CONFIG:
                        key = Microsoft.Win32.Registry.CurrentConfig.CreateSubKey(regPath);
                        break;
                    case RegistryParts.HKEY_CURRENT_USER:
                        key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(regPath);
                        break;
                    case RegistryParts.HKEY_LOCAL_MACHINE:
                        key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(regPath);
                        break;
                    case RegistryParts.HKEY_USERS:
                        key = Microsoft.Win32.Registry.Users.CreateSubKey(regPath);
                        break;
                }
                regValue = Convert.ToString(key.GetValue(regName));
                key.Close();
                return State.ErrorCode.ALL_RIGTH;
            }
            catch
            {
                return State.ErrorCode.FATAL_REG_ERROR;
            }
        }

    }
}
