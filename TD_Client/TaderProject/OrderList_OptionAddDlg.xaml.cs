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
using TaderProject.CModels;

namespace TaderProject
{
    /// <summary>
    /// OrderList_OptionAddDlg.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderList_OptionAddDlg : Window
    {
        HttpClient client = new HttpClient();

        string msguserid = "";
        string msgstorename = "";
        bool optionselect = false;

        //부모 -> 자식이벤트
        public delegate void OnChildDataInputHandler(int Parameters);
        public event OnChildDataInputHandler OnChildDataInputEvent;

        public OrderList_OptionAddDlg(string userid, string uristr, string menustr,string storenamestr)
        {
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

            msguserid = userid;
            Dlg_OrderMenuTB.Text = menustr;

            msgstorename = storenamestr;
            GetOrderOptionData(storenamestr);
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
            if (OnChildDataInputEvent != null) OnChildDataInputEvent(0); //이벤트 상수
            this.Close();
        }
        #endregion

        #region 주문 POST & 버튼
        //주문 POST
        private async void GetPostMenuOption(object sender, RoutedEventArgs e, string Userid)
        {
            try
            {
                var odinfo = new C_OrderInfo()
                {
                    UserName = Userid,
                    MenuName = Dlg_OrderMenuTB.Text,
                    OptionDes = Dlg_OptionTB.Text
                };
                var response = await client.PostAsJsonAsync("OrderInfo", odinfo);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();

            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Price must be a number");
            }

        }
        private void TakeOrderbtn_Click(object sender, RoutedEventArgs e)
        {
            GetPostMenuOption(sender, e, msguserid);
            if (OnChildDataInputEvent != null) OnChildDataInputEvent(1); //이벤트 상수
            this.Close();
        }
        #endregion

        #region 콤보박스 DATA GET
        private async void GetOrderOptionData(string storemsg)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderCombo/option/{0}", storemsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboOption>>();

                M_ComboOption.GetOption().Clear();
                foreach (var item in tests)
                {
                    M_ComboOption.GetOption().Add(new M_ComboOption { OptionCombo = item.OptionCombo });
                }
                Dlg_OptionCB.ItemsSource = M_ComboOption.GetOption();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
        }
        private async void GetOrderSelectOptionData(string storemsg)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderCombo/option/select/{0}", storemsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboOption>>();

                M_ComboOption.GetOption().Clear();
                foreach (var item in tests)
                {
                    M_ComboOption.GetOption().Add(new M_ComboOption { OptionCombo = item.OptionCombo });
                }
                Dlg_OptionCB.ItemsSource = M_ComboOption.GetOption();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
        }
        #endregion

        #region 옵션 Setting
        private void Dlg_OptionTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    optionselect = true;
                    Dlg_OptionCB.IsDropDownOpen = true;
                    if (Dlg_OptionTB.Text == M_ComboOption.GetOption()[0].OptionCombo)
                    {
                        Dlg_OptionCB.IsDropDownOpen = false;
                    }

                }
                else if (e.Key != Key.Enter)
                    Dlg_OptionCB.IsDropDownOpen = false;
            }
            catch (Exception)
            { }
        }
        private void OptionCB_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                Dlg_OptionCB.Focusable = true;
                Dlg_OptionCB.Focus();

                if (optionselect == false)
                {
                    GetOrderOptionData(msgstorename); ;
                }
                else if (optionselect == true)
                {
                    string msg = "";
                    msg += msgstorename;
                    msg += "&";
                    msg += Dlg_OptionTB.Text;
                    GetOrderSelectOptionData(msg);
                    optionselect = false;
                }
            }
            catch (Exception)
            { }
        }

        private void OptionCB_DropDownClosed(object sender, EventArgs e)
        {
            Dlg_OptionCB.Focusable = false;

            if (Dlg_OptionTB.Text != "")
            {
                Dlg_TakeOrderbtn.Focus();
            }
            else if (Dlg_OptionTB.Text == "")
            {
                Dlg_OptionTB.Focus();
            }
        }

        private void OptionCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Dlg_OptionTB.Text = M_ComboOption.GetOption()[Dlg_OptionCB.SelectedIndex].OptionCombo;
                Dlg_OptionCB.SelectedIndex = -1;
                Dlg_OptionCB.Text = "";
            }
            catch (Exception)
            {
                Dlg_OptionCB.Text = "";
            }
        }
        #endregion
    }
}