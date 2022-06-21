using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaderProject
{
    /// <summary>
    /// IPSettingDlg.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IPSettingDlg : Window
    {

        //Local File Address
        string strLocalPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf('\\')); //File Address

        private int closecheck = 0;

        // File IP 정보 확인 (불러오기/입력)
        public IPSettingDlg(int check)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            closecheck = check;
            #region ID File IO
            if (System.IO.File.Exists(strLocalPath + "\\SERVERIP.txt")) // 폴더 확인
            {
                string strReturnValue = System.IO.File.ReadAllText("SERVERIP.txt"); //폴더 불러오기

                if (strReturnValue == "")
                {
                    MessageBox.Show("불러오기 실패 ServerIP가 없습니다.");
                    return;
                }
                else 
                {
                    IPSettingTB.Text = strReturnValue;
                }
            }
            #endregion

        }

        #region 타이틀바 UI 이벤트
        // Mouse Move
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        // 최소화버튼
        private void ToMiniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        // 종료버튼
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (closecheck == 0)
            {
                MessageBoxResult msgcheck = MessageBox.Show("프로그램이 종료됩니다. 종료하시겠습니까?", "프로그램 종료", MessageBoxButton.YesNo);
                if (msgcheck == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
            else if (closecheck == 1)
            {
                MessageBoxResult msgcheck = MessageBox.Show("변경 내용이 저장되지 않습니다. 종료하시겠습니까?", "서버 IP 설정 종료", MessageBoxButton.YesNo);
                if (msgcheck == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }
        #endregion
        
        // 입력 IP Check
        private void IPSettingBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgcheck = MessageBox.Show("서버 IP를 정확히 입력하셨습니까?", "IP 확인", MessageBoxButton.YesNo);
            if (msgcheck == MessageBoxResult.Yes)
            {
                if (System.IO.File.Exists(strLocalPath + "\\SERVERIP.txt"))
                {
                    System.IO.File.Delete(strLocalPath + "\\SERVERIP.txt");
                }
                string strID = IPSettingTB.Text;
                System.IO.File.WriteAllText("SERVERIP.txt", strID);
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
