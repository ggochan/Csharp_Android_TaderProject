using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using System.Runtime.InteropServices;
using System.Threading;

namespace TaderProject
{
    /// <summary>
    /// OrderReceipt.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderReceipt : Window
    {
        HttpClient client = new HttpClient();

        string strLocalPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf('\\')); //File Address

        public delegate void recv_OnChildRecDataInputEvent(int Parameters);
        public event recv_OnChildRecDataInputEvent OnChildDataInputEvent;

        private int filecheck = 0;
        #region 카카오톡 메시지 전송 DLL
        [System.Runtime.InteropServices.DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("User32", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, string lParam);

        [DllImport("user32.dll")]
        public static extern uint PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        #endregion

        public OrderReceipt(string uristr)
        {
            InitializeComponent();

            #region 다이얼로그 창 위치
            //1모니터
            /* var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
             this.Left = desktopWorkingArea.Right/2 + 190;
             this.Top = desktopWorkingArea.Bottom/2 - 220;
             this.WindowStartupLocation = WindowStartupLocation.Manual;*/
            //2모니터
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            #endregion

            #region 클라이언트 세팅

            client.BaseAddress = new Uri(uristr);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            #endregion

            #region ID File IO
            if (System.IO.File.Exists(strLocalPath + "\\Receipt.txt")) // 폴더 확인
            {
                string strReturnValue = System.IO.File.ReadAllText("Receipt.txt"); //폴더 불러오기

                if (strReturnValue == "")
                {
                    MessageBox.Show("불러오기 실패 ID가 없습니다.");
                    return;
                }
                rec_id_TB.Text = strReturnValue;
            }
            #endregion

            if (rec_id_TB.Text == "") { filecheck = 1; }

            GetReseiptData();
        }
        #region 타이틀바 UI 이벤트
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ToMiniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 카카오톡 메시지 처리
        private void KaKaoTalkMessageTrans_btn(object sender, RoutedEventArgs e)
        {
            try{
                if (rec_id_TB.Text == "")
                {
                    MessageBox.Show("전송 받을 채팅방 이름을 입력해주세요.");
                }
                else
                {
                    if (filecheck == 1 && MessageBox.Show("채팅방에 전송 후 종료되며 주문내역이 삭제 됩니다.\n채팅방 명을 정확히 입력해주세요.\r계속 하시겠습니까?", "카카오톡 메시지 전송", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        sendMsg(recTB.Text, rec_id_TB.Text);
                        FileCheck();
                        if (OnChildDataInputEvent != null) OnChildDataInputEvent(0); //이벤트 상수
                        this.Close();
                    }
                    else
                    {
                        sendMsg(recTB.Text, rec_id_TB.Text);
                        FileCheck();
                        if (OnChildDataInputEvent != null) OnChildDataInputEvent(0); //이벤트 상수
                        this.Close();
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("해당되는 카카오톡 창을 띄워주세요.");
            }

        }
        private void sendMsg(string msg, string id)
        {
            IntPtr hd01 = FindWindow(null, id);
            if (hd01 != IntPtr.Zero)
            {
                IntPtr hd03 = FindWindowEx(hd01, IntPtr.Zero, "RichEdit50W", "");

                SendMessage(hd03, 0x000c, 0, msg);

                Thread.Sleep(100);

                PostMessage(hd03, 0x0100, 0xD, 0x1C001);
            }
        }
        #endregion

        #region  영수증 데이터 처리
        private async void GetReseiptData()
        {
            try
            {
                var response = await client.GetAsync("OrderInfo/reseipt");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();
                
                foreach (var item in tests) 
                {
                    string[] splstring = item.ToString().Split('&');
                    recTB.Text = Str_return(splstring);
                }
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
        }
        // 영수증 편집
        private string Str_return(string[] str)
        {
            string returnstr = "";
            string strop = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (i % 3 == 0 && i != 0 && str[i] != "") // 메뉴
                {
                    returnstr += "\n메뉴 : " + str[i] + "\n";
                }
                else if (i == 0 && str[i] != "") // 메뉴
                {
                    returnstr += "메뉴 : " + str[i] + "\n";
                }
                else if (i % 3 == 1 && i != 0 && str[i] != "") //수량
                {
                    returnstr += "수량 : " + str[i];
                    returnstr += "\n";
                }
                else if (i % 3 == 2 && i != 0 && str[i] != "") // 옵션
                {
                    string[] optionstr = str[i].Split('@');
                    strop = "";
                    int ij = 1;
                    for (int j = 0; j < optionstr.Length; j++)
                    {
                        if (optionstr[j] != "x" && optionstr[j] != "")
                        {
                            if (ij == 1)
                            {
                                strop += string.Format("옵션({0}) : {1}", ij.ToString(), optionstr[j]);
                                ij++;
                            }
                            else
                            {
                                strop += string.Format("\n옵션({0}) : {1}", ij.ToString(), optionstr[j]);
                                ij++;
                            }
                        }

                    }
                    returnstr += strop;
                    if (i == (str.Length-1) || strop == "")
                    {
                        
                    }
                    else
                    {
                        returnstr += "\n";
                    }
                    returnstr += "-----------------------------";
                }
            }
            return returnstr;
        }

        // 주문 받은 사람 이름
        private void FileCheck()
        {
            if (filecheck == 1)
            {
                string strID = rec_id_TB.Text;
                System.IO.File.WriteAllText("Receipt.txt", strID);
                filecheck = 0;
            }
            else
            {
                    if (System.IO.File.Exists(strLocalPath + "\\Receipt.txt"))
                    {
                        System.IO.File.Delete(strLocalPath + "\\Receipt.txt");
                    }
                    string strID = rec_id_TB.Text;
                    System.IO.File.WriteAllText("Receipt.txt", strID);
                
            }
        }
        #endregion
    }
}
