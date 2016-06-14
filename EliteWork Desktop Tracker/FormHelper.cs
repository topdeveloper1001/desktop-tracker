using EliteWork_Desktop_Tracker.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EliteWork_Desktop_Tracker
{
    class FormHelper
    {
        private static Dictionary<string, Bitmap> _ButtonImages = null;
        private static Dictionary<string, Bitmap> _ButtonOverImages = null;

        public static Bitmap GetButtonImage(Button button)
        {
            if (_ButtonImages == null)
            {
                _ButtonImages = new Dictionary<string, Bitmap>();
                _ButtonImages.Add("_goback_settings_btn", Resources.settings);
                _ButtonImages.Add("_goback_log_btn", Resources.settings);
                _ButtonImages.Add("_clear_cache_btn", Resources.log_button_1); 
                _ButtonImages.Add("_activity_log_btn", Resources.log_button_1);
                _ButtonImages.Add("_report_bugs_btn", Resources.log_button_1);
                _ButtonImages.Add("_goback_btn", Resources.settings);
                _ButtonImages.Add("_download_now_btn", Resources.download_now_button);
                _ButtonImages.Add("_start_btn", Resources.start_button);
                _ButtonImages.Add("_log_btn", Resources.log_button_2);
                _ButtonImages.Add("_settings_btn", Resources.settings);
                _ButtonImages.Add("_login_btn", Resources.login_button2);
                _ButtonImages.Add("_log_lf_btn", Resources.log_button_1); 
                _ButtonImages.Add("_submit_bug_btn", Resources.settings);
                _ButtonImages.Add("_goback_bug_btn", Resources.settings);
            }

            return _ButtonImages[button.Name];
        }

        public static Bitmap GetButtonOverImage(Button button)
        {
            if (_ButtonOverImages == null)
            {
                _ButtonOverImages = new Dictionary<string, Bitmap>();
                _ButtonOverImages.Add("_goback_settings_btn", Resources.settings_over);
                _ButtonOverImages.Add("_goback_log_btn", Resources.settings_over);
                _ButtonOverImages.Add("_clear_cache_btn", Resources.log_button_1_over);
                _ButtonOverImages.Add("_activity_log_btn", Resources.log_button_1_over);
                _ButtonOverImages.Add("_report_bugs_btn", Resources.log_button_1_over);
                _ButtonOverImages.Add("_goback_btn", Resources.settings_over);
                _ButtonOverImages.Add("_download_now_btn", Resources.download_now_button_over);
                _ButtonOverImages.Add("_start_btn", Resources.start_button_over);
                _ButtonOverImages.Add("_log_btn", Resources.log_button_2_over);
                _ButtonOverImages.Add("_settings_btn", Resources.settings_over);
                _ButtonOverImages.Add("_login_btn", Resources.login_button_over2);
                _ButtonOverImages.Add("_log_lf_btn", Resources.log_button_1_over);
                _ButtonOverImages.Add("_submit_bug_btn", Resources.settings_over); 
                _ButtonOverImages.Add("_goback_bug_btn", Resources.settings_over);
            }

            return _ButtonOverImages[button.Name];
        }
    }
}
