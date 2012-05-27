using System;
using System.Globalization;
using System.Windows;

namespace ItshoMultiVisualizer
{
    internal static class UiHelper
    {
        public const string APP_NAME = "ItshoMultiVisualizer";

        public static MessageBoxResult MyMessageBox(string p_strMessage)
        {
            return MessageBox.Show(p_strMessage, APP_NAME, MessageBoxButton.OK, MessageBoxImage.None,
                                   MessageBoxResult.OK);
        }

        public static MessageBoxResult MyMessageBox(string p_strMessage, MessageBoxButton p_objButtons,MessageBoxImage p_objImage = MessageBoxImage.Question)
        {
            return MessageBox.Show(p_strMessage, APP_NAME,
                                   p_objButtons,
                                   p_objImage,
                                   MessageBoxResult.OK);
        }

        public static void MyMessageBox(string p_strMessage, Exception ex)
        {
            MessageBox.Show(p_strMessage + Environment.NewLine + ex.Message,
                            APP_NAME,
                            MessageBoxButton.OK,
                            MessageBoxImage.Error,
                            MessageBoxResult.OK);
        }

        /// <summary>
        /// Is currenct langauge used is Right-To-Left
        /// http://blogs.msdn.com/b/vsarabic/archive/2010/04/19/detecting-if-your-locale-culture-is-righttoleft.aspx
        /// </summary>
        /// <returns></returns>
        public static bool IsCurrentlanguageRTL()
        {
            return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
        }
    }
}
