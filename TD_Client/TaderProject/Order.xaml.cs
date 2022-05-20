using System;
using System.Collections.Generic;
using System.Data;
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
    /// Order.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Order : Window
    {
        HttpClient client = new HttpClient();

        private string Userid = "";

        // 자식 -> 부모 데이터 보내기 이벤트
        public delegate void OnChildDataInputHandler(int Parameters);
        public event OnChildDataInputHandler OnChildDataInputEvent;

        private bool kindselect = false, storeselect = false, menuselect = false, optionselect = false;
        public Order(string id,string uristr)
        {
            #region 공용 Window Setting
            InitializeComponent();
            //TestBox.Text = a;
            #endregion

            #region UI Value
            FoodKindTB.MaxLength = 30;
            FoodKindTB.Focus();
            StoreNameTB.MaxLength = 30;
            OrderMenuTB.MaxLength = 30;
            OptionTB.MaxLength = 60;

            Userid = id;
            for (int i = 2; i < 100; i++) { UserCountCB.Items.Add(i); }
            #endregion

            #region 클라이언트 세팅

            client.BaseAddress = new Uri(uristr);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            #endregion 
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
            MessageBoxResult msgcheck1 = MessageBox.Show("주문이 완료 되지 않았습니다. 나가시겠습니까?","주문완료", MessageBoxButton.YesNo);
            if (msgcheck1 == MessageBoxResult.Yes)
            {
                if (OnChildDataInputEvent != null) OnChildDataInputEvent(0);
                this.Close();
            }
        }
        #endregion

        #region 주문 요청 버튼
        // 주문
        private void OrderClearbtn(object sender, RoutedEventArgs e)
        {
            if (UserCountCB.Text != "")
            {
                if (FoodKindTB.Text != "")
                {
                    if (StoreNameTB.Text != "")
                    {
                        GetPostKindStore(sender, e);
                        GetPostMenuOption(sender, e, Userid);
                        if (OnChildDataInputEvent != null) OnChildDataInputEvent(2); //이벤트 상수
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("가게명을 입력해 주세요.");
                    }
                }
                else 
                {
                    MessageBox.Show("종류를 입력해 주세요.");
                }
            }
            else if(UserCountCB.Text == "")
            {
                MessageBox.Show("인원 수를 입력해주세요.");
            }

        }
        #endregion

        #region Main Post
        // 종류, 가게명, 인원 POST
        private async void GetPostKindStore(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var odlist = new C_Orderlist()
                {
                    KindName = FoodKindTB.Text,
                    StoreName = StoreNameTB.Text,
                    Count = int.Parse(UserCountCB.SelectedItem.ToString()),
            };
                var response = await client.PostAsJsonAsync("OrderList", odlist);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();

            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("변환에러");
            } 
        }
        //메뉴 , 옵션 POST
        private async void GetPostMenuOption(object sender, RoutedEventArgs e,string Userid)
        {
            try
            {
                var odinfo = new C_OrderInfo()
                {
                    UserName = Userid,
                    MenuName = OrderMenuTB.Text,
                    OptionDes = OptionTB.Text
                };
                var response = await client.PostAsJsonAsync("OrderInfo", odinfo);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();
                foreach (var item in tests)
                {
                    string[] splstring = item.ToString().Split('&');

                   // MessageBox.Show("UserID = " + splstring[0] + "    메뉴 = " + splstring[1] + "    옵션 = " + splstring[2]);

                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Price must be a number");
            }
            /*finally
            {
                btnPost.IsEnabled = true;
            }*/
        }
        #endregion

        #region ComboBox setting
        private async void GetOrderKindDataAll()
        {
            try
            {
                var response = await client.GetAsync("OrderCombo/allk");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboKind>>();

                M_ComboKind.GetKind().Clear();
                foreach (var item in tests)
                {
                    M_ComboKind.GetKind().Add(new M_ComboKind { ComboKind = item.ComboKind });
                }
                FoodKindCB.ItemsSource = M_ComboKind.GetKind();
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

        private async void GetOrderKindData(string kindmsg)
        {
            try
            {                
                var response = await client.GetAsync(string.Format("OrderCombo/kindname/{0}", kindmsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboKind>>();

                M_ComboKind.GetKind().Clear();
                foreach (var item in tests)
                {
                    M_ComboKind.GetKind().Add(new M_ComboKind { ComboKind = item.ComboKind});
                }
                FoodKindCB.ItemsSource = M_ComboKind.GetKind();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
            }
        }
        private async void GetOrderStoreData(string kindmsg)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderCombo/storename/{0}",kindmsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboStore>>();

                M_ComboStore.GetStore().Clear();
                foreach (var item in tests)
                {
                    M_ComboStore.GetStore().Add(new M_ComboStore { ComboStore = item.ComboStore });
                }
                StoreNameCB.ItemsSource = M_ComboStore.GetStore();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("종류명을 먼저 선택하거나 입력해주세요.");
            }
        }
        private async void GetOrderSelectStoreData(string msg)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderCombo/storename/select/{0}", msg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboStore>>();

                M_ComboStore.GetStore().Clear();
                foreach (var item in tests)
                {
                    M_ComboStore.GetStore().Add(new M_ComboStore { ComboStore = item.ComboStore });
                }
                StoreNameCB.ItemsSource = M_ComboStore.GetStore();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("종류명을 먼저 선택하거나 입력해주세요.");
            }
        }
        private async void GetOrderMenuData(string storemsg)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderCombo/menu/{0}", storemsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboMenu>>();

                M_ComboMenu.GetMenu().Clear();
                foreach (var item in tests)
                {
                    M_ComboMenu.GetMenu().Add(new M_ComboMenu { MenuCombo = item.MenuCombo });
                }
                OderMenuCB.ItemsSource = M_ComboMenu.GetMenu();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("종류와 가게명을 먼저 선택하거나 입력해주세요.");
            }
        }
        private async void GetOrderSelectMenuData(string storemsg)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderCombo/menu/select/{0}", storemsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboMenu>>();

                M_ComboMenu.GetMenu().Clear();
                foreach (var item in tests)
                {
                    M_ComboMenu.GetMenu().Add(new M_ComboMenu { MenuCombo = item.MenuCombo });
                }
                OderMenuCB.ItemsSource = M_ComboMenu.GetMenu();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("종류와 가게명을 먼저 선택하거나 입력해주세요.");
            }
        }
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
                OptionCB.ItemsSource = M_ComboOption.GetOption();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("종류와 가게명을 먼저 선택하거나 입력해주세요.");
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
                OptionCB.ItemsSource = M_ComboOption.GetOption();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("종류와 가게명을 먼저 선택하거나 입력해주세요.");
            }
        }
        #endregion

        #region 콤보박스 체인지 이벤트
        private void FoodKindCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FoodKindTB.Text = M_ComboKind.GetKind()[FoodKindCB.SelectedIndex].ComboKind;
                FoodKindCB.SelectedIndex = -1;
                FoodKindCB.Text = "";
            }
            catch(Exception)
            {
                FoodKindCB.Text = "";
            }
        }

        private void StoreNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StoreNameTB.Text = M_ComboStore.GetStore()[StoreNameCB.SelectedIndex].ComboStore;
                StoreNameCB.SelectedIndex = -1;
                StoreNameCB.Text = "";
            }
            catch (Exception)
            {
                StoreNameCB.Text = "";
            }
        }

        private void OderMenuCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OrderMenuTB.Text = M_ComboMenu.GetMenu()[OderMenuCB.SelectedIndex].MenuCombo;
                OderMenuCB.SelectedIndex = -1;
                OderMenuCB.Text = "";
            }
            catch (Exception)
            {
                OderMenuCB.Text = "";
            }
        }
        private void OptionCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OptionTB.Text = M_ComboOption.GetOption()[OptionCB.SelectedIndex].OptionCombo;
                OptionCB.SelectedIndex = -1;
                OptionCB.Text = "";
            }
            catch (Exception)
            {
                OptionCB.Text = "";
            }
        }
        #endregion

        #region TB Focus 이벤트
        private void StoreGetFocus(object sender, RoutedEventArgs e)
        {
            if (FoodKindTB.Text == "")
            {
                MessageBox.Show("종류가 입력되지 않았습니다. 입력해주세요");
                FoodKindTB.Focus();
            }
            else if(FoodKindTB.Text != "")
            {
             
            }
        }
        private void MenuGetFocus(object sender, RoutedEventArgs e)
        {
            if (FoodKindTB.Text == "")
            {
                MessageBox.Show("종류가 입력되지 않았습니다. 입력해주세요");
                FoodKindTB.Focus();
            }
            else if(FoodKindTB.Text != "")
            {
                if (StoreNameTB.Text == "")
                {
                    MessageBox.Show("가게명이 입력되지 않았습니다. 입력해주세요");
                    StoreNameTB.Focus();
                }
                else if (StoreNameTB.Text != "")
                {

                }
            }
        }

        private void OptionGetFocus(object sender, RoutedEventArgs e)
        {
            if (FoodKindTB.Text == "")
            {
                MessageBox.Show("종류가 입력되지 않았습니다. 입력해주세요");
                FoodKindTB.Focus();
            }
            else if (FoodKindTB.Text != "")
            {
                if (StoreNameTB.Text == "")
                {
                    MessageBox.Show("가게명이 입력되지 않았습니다. 입력해주세요");
                    StoreNameTB.Focus();
                }
                else if (StoreNameTB.Text != "")
                {

                }
            }
        }
        #endregion

        #region TB KeyDown + (CB DropDown + DropClosed) 이벤트

        #region 종류
        private void FoodKindTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (FoodKindTB.Text != "")
                    {
                        kindselect = true;
                    }
                    FoodKindCB.IsDropDownOpen = true;
                    if (FoodKindTB.Text == M_ComboKind.GetKind()[0].ComboKind)
                    {
                        FoodKindCB.IsDropDownOpen = false;
                    }

                }
                else if (e.Key != Key.Enter)
                {
                    FoodKindCB.IsDropDownOpen = false;
                }
            }
            catch (Exception)
            { }
        }
        private void FoodKindCB_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                StoreNameTB.Text = "";
                OrderMenuTB.Text = "";
                OptionTB.Text = "";

                FoodKindCB.Focusable = true;
                FoodKindCB.Focus();

                if (kindselect == false)
                 {
                     GetOrderKindDataAll();
                  }
                else if (kindselect == true)
                 {
                    GetOrderKindData(FoodKindTB.Text.Trim());
                    kindselect = false;
                 }    
            }
            catch (Exception)
            { }
        }
        private void FoodKindCB_DropDownClosed(object sender, EventArgs e)
        {
            StoreNameCB.Focusable = false;
            
            if(FoodKindTB.Text != "")
                StoreNameTB.Focus();
            else if (FoodKindTB.Text == "")
                FoodKindTB.Focus();
        }
        #endregion

        #region 가게명
        private void StoreNameTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    storeselect = true;
                    StoreNameCB.IsDropDownOpen = true;
                    if (StoreNameTB.Text == M_ComboStore.GetStore()[0].ComboStore)
                    {
                        StoreNameCB.IsDropDownOpen = false;
                    }
                }
                else if (e.Key != Key.Enter)
                    StoreNameCB.IsDropDownOpen = false;
            }
            catch (Exception)
            { }
        }
        private void StoreNameCB_DropDownOpened(object sender, EventArgs e)
        { 
            try
            {
                OrderMenuTB.Text = "";
                OptionTB.Text = "";

                StoreNameCB.Focusable = true;
                StoreNameCB.Focus();

                if (storeselect == false) {
                    GetOrderStoreData(FoodKindTB.Text.Trim());
                }
                else if (storeselect == true)
                {
                    string msg = "";
                    msg += FoodKindTB.Text;
                    msg += "&";
                    msg += StoreNameTB.Text;
                    GetOrderSelectStoreData(msg);
                    storeselect = false;
                }
            }
            catch (Exception)
            { }
        }
        private void StoreNameCB_DropDownClosed(object sender, EventArgs e)
        {
            StoreNameCB.Focusable = false;
            if (StoreNameTB.Text != "")
                OrderMenuTB.Focus();
            else if (StoreNameTB.Text == "")
                StoreNameTB.Focus();
        }
        #endregion

        #region 메뉴
        private void OrderMenuTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    menuselect = true;
                    OderMenuCB.IsDropDownOpen = true;
                    if (OrderMenuTB.Text == M_ComboMenu.GetMenu()[0].MenuCombo)
                    {
                        OderMenuCB.IsDropDownOpen = false;
                    }
                }
                else if (e.Key != Key.Enter)
                    OderMenuCB.IsDropDownOpen = false;
            }
            catch (Exception)
            { }
        }
        private void OderMenuCB_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                OderMenuCB.Focusable = true;
                OderMenuCB.Focus();

                if (menuselect == false)
                {
                    GetOrderMenuData(StoreNameTB.Text.Trim());
                }
                else if (menuselect == true)
                {
                    string msg = "";
                    msg += StoreNameTB.Text;
                    msg += "&";
                    msg += OrderMenuTB.Text;
                    GetOrderSelectMenuData(msg);
                    menuselect = false;
                }
            }
            catch (Exception)
            { }
        }
        private void OderMenuCB_DropDownClosed(object sender, EventArgs e)
        {
            OderMenuCB.Focusable = false;
            if (OrderMenuTB.Text != "")
                OptionTB.Focus();
            else if (OrderMenuTB.Text == "")
                OrderMenuTB.Focus();
        }

        #endregion

        #region 옵션
        private void OptionTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    optionselect = true;
                    OptionCB.IsDropDownOpen = true;
                    if (OptionTB.Text == M_ComboOption.GetOption()[0].OptionCombo)
                    {
                        OptionCB.IsDropDownOpen = false;
                    }

                }
                else if (e.Key != Key.Enter)
                    OptionCB.IsDropDownOpen = false;
            }
            catch (Exception)
            { }
        }
        private void OptionCB_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                OptionCB.Focusable = true;
                OptionCB.Focus();

                if (optionselect == false)
                {
                    GetOrderOptionData(StoreNameTB.Text.Trim()); ;
                }
                else if (optionselect == true)
                {
                    string msg = "";
                    msg += StoreNameTB.Text;
                    msg += "&";
                    msg += OptionTB.Text;
                    GetOrderSelectOptionData(msg);
                    optionselect = false;
                }
            }
            catch (Exception)
            { }
        }
        private void OptionCB_DropDownClosed(object sender, EventArgs e)
        {
            OptionCB.Focusable = false;

            if (OptionTB.Text != "")
            {
                UserCountCB.Focus();
                UserCountCB.IsDropDownOpen = true;
            }
            else if (OptionTB.Text == "")
            {
                OptionTB.Focus();
            }
        }
        #endregion

        private void UserCountCB_DropDownOpened(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
