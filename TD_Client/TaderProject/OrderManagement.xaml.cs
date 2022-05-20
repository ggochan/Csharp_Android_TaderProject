using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// OrderManagement.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderManagement : Window
    {
        HttpClient client = new HttpClient();

        bool kindentercheck = false;
        bool storeentercheck = false;
        bool menuentercheck = false;
        bool optionentercheck = false;
        public OrderManagement(string uristr)
        {
            InitializeComponent();

            #region 클라이언트 세팅

            client.BaseAddress = new Uri(uristr);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            #endregion

            M_PastAllCB.Items.Add("유저 ID");
            M_PastAllCB.Items.Add("종류");
            M_PastAllCB.Items.Add("가게명");

            GetOrderKindData();
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

        #region 종류
        // 모든 종류 불러오기
        private async void GetOrderKindData()
        {
            try
            {
                var response = await client.GetAsync("OrderManager/kindname");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboKind>>();

                M_ComboKind.GetKind().Clear();

                M_ComboStore.GetStore().Clear();
                M_StoreNameLi.Items.Refresh();
                M_StoreNameTB.Text = "";

                M_ComboMenuPrice.GetMenuprice().Clear();
                M_MenuNameLi.Items.Refresh();
                M_MenuNameTB.Text = "";
                M_MenuPriceTB.Text = "";

                M_ComboOptionPrice.GetOptionprice().Clear();
                M_OptionLi.Items.Refresh();
                M_OptionTB.Text = "";
                M_OptionPriceTB.Text = "";

                foreach (var item in tests)
                {
                    M_ComboKind.GetKind().Add(new M_ComboKind { ComboKind = item.ComboKind });
                }
                M_FoodKindLi.ItemsSource = M_ComboKind.GetKind();
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
        // 종류 키이벤트
        private void M_FoodKindTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Back)
                {
                    int i;
                    for (i = 0; i < M_ComboKind.GetKind().Count; i++)
                    {
                        if (M_ComboKind.GetKind()[i].ComboKind == M_FoodKindTB.Text || M_ComboKind.GetKind()[i].ComboKind.Contains(M_FoodKindTB.Text) == true)
                        {
                            kindentercheck = true;
                            break;
                        }
                    }
                    M_FoodKindLi.SelectedIndex = i;
                    GetOrderStoreData(M_FoodKindTB.Text.Trim());

                    M_StoreNameTB.Text = "";

                    M_ComboMenuPrice.GetMenuprice().Clear();
                    M_MenuNameLi.Items.Refresh();
                    M_MenuNameTB.Text = "";
                    M_MenuPriceTB.Text = "";

                    M_ComboOptionPrice.GetOptionprice().Clear();
                    M_OptionLi.Items.Refresh();
                    M_OptionTB.Text = "";
                    M_OptionPriceTB.Text = "";
                }
            }
            catch (Exception) { }
        }
        // 종류 리스트 클릭 이벤트
        private void M_FoodKindLi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (kindentercheck == false)
                {
                    if (M_PastAllLI.SelectedIndex == -1)
                    {
                        M_FoodKindTB.Text = M_ComboKind.GetKind()[M_FoodKindLi.SelectedIndex].ComboKind;
                        GetOrderStoreData(M_FoodKindTB.Text.Trim());

                        M_StoreNameLi.Items.Refresh();
                        M_StoreNameTB.Text = "";

                        M_ComboMenuPrice.GetMenuprice().Clear();
                        M_MenuNameLi.Items.Refresh();
                        M_MenuNameTB.Text = "";
                        M_MenuPriceTB.Text = "";

                        M_ComboOptionPrice.GetOptionprice().Clear();
                        M_OptionLi.Items.Refresh();
                        M_OptionTB.Text = "";
                        M_OptionPriceTB.Text = "";

                    }
                    else
                    {
                        M_FoodKindTB.Text = M_ComboKind.GetKind()[M_FoodKindLi.SelectedIndex].ComboKind;
                        GetOrderStoreData(M_FoodKindTB.Text.Trim());
                        M_StoreNameLi.Items.Refresh();

                        M_ComboMenuPrice.GetMenuprice().Clear();
                        M_MenuNameLi.Items.Refresh();

                        M_ComboOptionPrice.GetOptionprice().Clear();
                        M_OptionLi.Items.Refresh();
                        M_PastAllLI.SelectedItem = null;
                    }
                }
                else if (kindentercheck == true)
                {
                    kindentercheck = false;
                }
            }
            catch(Exception){}
        }
        // 종류 저장
        private void M_FoodKindAdd_Click(object sender, RoutedEventArgs e)
        {
            try { 
                bool check = false;
                for(int i =0; i<M_ComboKind.GetKind().Count; i++)
                {
                    if(M_ComboKind.GetKind()[i].ComboKind == M_FoodKindTB.Text)
                    {
                        check = true;
                        break;
                    }
                }
                if (check == false)
                {
                    PostKindInsert(sender, e);
                    M_ComboKind.GetKind().Add(new M_ComboKind { ComboKind = M_FoodKindTB.Text });
                    M_FoodKindLi.Items.Refresh();
                    M_FoodKindLi.SelectedIndex = M_ComboKind.GetKind().Count - 1;
                }
                else
                {
                    MessageBox.Show("같은 종류 명이 이미 있습니다");
                }
            }
            catch (Exception) { }
        }
        private async void PostKindInsert(object sender, RoutedEventArgs e)
        {
            try
            {
                var kind = new M_ComboKind()
                {
                    ComboKind = M_FoodKindTB.Text
                };
                var response = await client.PostAsJsonAsync("OrderManager/kindname/insert", kind);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();
                
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("저장에러");
            }
        }
        // 종류 수정
        private void M_FoodKindUpd_Click(object sender, RoutedEventArgs e)
        {
            try { 
                string upstr = "";
                upstr += M_FoodKindTB.Text;
                upstr += "&";
                upstr += M_ComboKind.GetKind()[M_FoodKindLi.SelectedIndex].ComboKind;

                PostKindUpdate(sender, e, upstr);

                M_ComboKind.GetKind()[M_FoodKindLi.SelectedIndex].ComboKind = M_FoodKindTB.Text;
                M_FoodKindLi.Items.Refresh();
            }
            catch (Exception) { }
        }
        private async void PostKindUpdate(object sender, RoutedEventArgs e, string upstr)
        {
            try
            {
                var kind = new M_ComboKind()
                {
                    ComboKind = upstr
                };
                var response = await client.PostAsJsonAsync("OrderManager/kindname/update", kind);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("수정에러");
            }
        }
        // 종류 삭제
        private void M_FoodKindDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("선택하신 종류가 삭제 됩니다.\r계속 하시겠습니까?", "종류 삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    PostKindDelete(sender, e);
                    if (M_FoodKindLi.SelectedItems.Count > 0)
                    {
                        M_ComboKind.GetKind().RemoveAt(M_FoodKindLi.SelectedIndex);
                        M_FoodKindTB.Text = "";
                    }
                    else
                    {
                        M_ComboKind.GetKind().RemoveAt(M_ComboKind.GetKind().IndexOf(new M_ComboKind() { ComboKind = M_FoodKindTB.Text }));
                        M_FoodKindTB.Text = "";
                    }
                }
                M_FoodKindLi.Items.Refresh();

                M_ComboStore.GetStore().Clear();
                M_StoreNameLi.Items.Refresh();
                M_StoreNameTB.Text = "";

                M_ComboMenuPrice.GetMenuprice().Clear();
                M_MenuNameLi.Items.Refresh();
                M_MenuNameTB.Text = "";
                M_MenuPriceTB.Text = "";

                M_ComboOptionPrice.GetOptionprice().Clear();
                M_OptionLi.Items.Refresh();
                M_OptionTB.Text = "";
                M_OptionPriceTB.Text = "";
            }
            catch (Exception) 
            {
                MessageBox.Show("삭제실패");
                    }
        }
        private async void PostKindDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                var kind = new M_ComboKind()
                {
                    ComboKind = M_FoodKindTB.Text
                };
                var response = await client.PostAsJsonAsync("OrderManager/kindname/delete", kind);
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
        #endregion

        #region 가게명
        // 종류로 가게명 불러오기
        private async void GetOrderStoreData(string kindmsg)
        {
            try
            {
               
                var response = await client.GetAsync(string.Format("OrderManager/storename/{0}", kindmsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.
       
                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboStore>>();

                M_ComboStore.GetStore().Clear();

                if (M_PastAllLI.SelectedIndex == -1)
                {
                    M_ComboMenuPrice.GetMenuprice().Clear();
                    M_MenuNameLi.Items.Refresh();
                    M_MenuNameTB.Text = "";
                    M_MenuPriceTB.Text = "";

                    M_ComboOptionPrice.GetOptionprice().Clear();
                    M_OptionLi.Items.Refresh();
                    M_OptionTB.Text = "";
                    M_OptionPriceTB.Text = "";
                }
                else
                {
                    M_ComboMenuPrice.GetMenuprice().Clear();
                    M_MenuNameLi.Items.Refresh();

                    M_ComboOptionPrice.GetOptionprice().Clear();
                    M_OptionLi.Items.Refresh();
                }
                foreach (var item in tests)
                {
                    M_ComboStore.GetStore().Add(new M_ComboStore { ComboStore = item.ComboStore });
                }
                M_StoreNameLi.ItemsSource = M_ComboStore.GetStore();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
               // MessageBox.Show("서버 연결이 되있지 않습니다");
            }
        }
        // 가게명 키이벤트
        private void M_FoodStoreTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Back)
                {
                    int i;
                    for (i = 0; i < M_ComboStore.GetStore().Count; i++)
                    {
                        if (M_ComboStore.GetStore()[i].ComboStore == M_StoreNameTB.Text || M_ComboStore.GetStore()[i].ComboStore.Contains(M_StoreNameTB.Text) == true)
                        {
                            storeentercheck = true;
                            break;
                        }
                    }
                    M_StoreNameLi.SelectedIndex = i;
                    GetOrderMenuData(M_StoreNameTB.Text.Trim());
                    GetOrderOptionData(M_StoreNameTB.Text.Trim());
                }
            }
            catch (Exception) { }
        }
        // 가게명 리스트 클릭 이벤트
        private void M_FoodStoreLi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (storeentercheck == false)
                {
                    if (M_PastAllLI.SelectedIndex == -1)
                    {
                        M_StoreNameTB.Text = M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;
                        GetOrderMenuData(M_StoreNameTB.Text.Trim());
                        GetOrderOptionData(M_StoreNameTB.Text.Trim());

                        M_MenuNameLi.Items.Refresh();
                        M_MenuNameTB.Text = "";
                        M_MenuPriceTB.Text = "";

                        M_OptionLi.Items.Refresh();
                        M_OptionTB.Text = "";
                        M_OptionPriceTB.Text = "";
                    }
                    else 
                    {
                        M_StoreNameTB.Text = M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;
                        GetOrderMenuData(M_StoreNameTB.Text.Trim());
                        GetOrderOptionData(M_StoreNameTB.Text.Trim());
                        M_MenuNameLi.Items.Refresh();
                        M_OptionLi.Items.Refresh();
                    }
                }
                else if (storeentercheck == true)
                {
                    storeentercheck = false;
                }
            }
            catch (Exception) { }
        }
        // 가게명 저장
        private void M_StoreNameAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool check = false;
                for (int i = 0; i < M_ComboStore.GetStore().Count; i++)
                {
                    if (M_ComboStore.GetStore()[i].ComboStore == M_StoreNameTB.Text)
                    {
                        check = true;
                        break;
                    }
                }
                if (check == false)
                {
                    string insertstr = "";
                    insertstr += M_StoreNameTB.Text;
                    insertstr += "&";
                    insertstr += M_ComboKind.GetKind()[M_FoodKindLi.SelectedIndex].ComboKind;

                    PostStoreInsert(sender, e, insertstr);
                    M_ComboStore.GetStore().Add(new M_ComboStore { ComboStore = M_StoreNameTB.Text });
                    M_StoreNameLi.Items.Refresh();
                    M_StoreNameLi.SelectedIndex = M_ComboStore.GetStore().Count - 1;
                }
                else
                {
                    MessageBox.Show("같은 가게명이 이미 있습니다");
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("선택된 리스트가 없습니다.");
            }
        }
        // 가게명 저장 POST
        private async void PostStoreInsert(object sender, RoutedEventArgs e, string insertstr)
        {
            try
            {
                var store = new M_ComboStore()
                {
                    ComboStore = insertstr
                };
                var response = await client.PostAsJsonAsync("OrderManager/storename/insert", store);
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
        // 가게명 수정
        private void M_StoreNameUpd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string upstr = "";
                upstr += M_StoreNameTB.Text;
                upstr += "&";
                upstr += M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;
                upstr += "&";
                upstr += M_ComboKind.GetKind()[M_FoodKindLi.SelectedIndex].ComboKind;

                PostStoreUpdate(sender, e, upstr);

                M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore = M_StoreNameTB.Text;
                M_StoreNameLi.Items.Refresh();
            }
            catch (Exception) { }
}
        // 가게명 수정 POST
        private async void PostStoreUpdate(object sender, RoutedEventArgs e, string upstr)
        {
            try
            {
                var store = new M_ComboStore()
                {
                    ComboStore = upstr
                };
                var response = await client.PostAsJsonAsync("OrderManager/storename/update", store);
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
        // 가게명 삭제

        private void M_StoreNameDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("선택하신 가게명이 삭제 됩니다.\r계속 하시겠습니까?", "종류 삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string deletestr = "";
                    deletestr += M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;
                    deletestr += "&";
                    deletestr += M_ComboKind.GetKind()[M_FoodKindLi.SelectedIndex].ComboKind;

                    PostStoreDelete(sender, e, deletestr);
                    if (M_StoreNameLi.SelectedItems.Count > 0)
                    {
                        M_ComboStore.GetStore().RemoveAt(M_StoreNameLi.SelectedIndex);
                        M_StoreNameTB.Text = "";
                    }
                    else
                    {
                        M_ComboStore.GetStore().RemoveAt(M_ComboStore.GetStore().IndexOf(new M_ComboStore() { ComboStore = M_StoreNameTB.Text }));
                        M_StoreNameTB.Text = "";
                    }
                }
                M_StoreNameLi.Items.Refresh();
                M_StoreNameTB.Text = "";

                M_ComboMenuPrice.GetMenuprice().Clear();
                M_MenuNameLi.Items.Refresh();
                M_MenuNameTB.Text = "";
                M_MenuPriceTB.Text = "";

                M_ComboOptionPrice.GetOptionprice().Clear();
                M_OptionLi.Items.Refresh();
                M_OptionTB.Text = "";
                M_OptionPriceTB.Text = "";
            }
            catch (Exception) { }
        }
        // 가게명 삭제 POST
        private async void PostStoreDelete(object sender, RoutedEventArgs e, string deletestr)
        {
            try
            {
                var store = new M_ComboStore()
                {
                    ComboStore = deletestr
                };
                var response = await client.PostAsJsonAsync("OrderManager/storename/delete", store);
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
        #endregion

        #region 메뉴 
        // 메뉴
        private async void GetOrderMenuData(string storemsg)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderManager/menu/{0}", storemsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.
                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboMenuPrice>>();

                M_ComboMenuPrice.GetMenuprice().Clear();
                M_MenuNameLi.Items.Refresh();
                if (M_PastAllLI.SelectedIndex == -1)
                {
                    M_MenuNameTB.Text = "";
                    M_MenuPriceTB.Text = "";
                }
                

                foreach (var item in tests)
                {
                    M_ComboMenuPrice.GetMenuprice().Add(new M_ComboMenuPrice { ComboMenu = item.ComboMenu, ComboMenuPrice = item.ComboMenuPrice });
                }
                M_MenuNameLi.ItemsSource = M_ComboMenuPrice.GetMenuprice();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                // MessageBox.Show("서버 연결이 되있지 않습니다");
            }
        }
        // 메뉴 키이벤트
        private void M_MenuNameTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Back)
                {
                    int i;
                    for (i = 0; i < M_ComboMenuPrice.GetMenuprice().Count; i++)
                    {
                        if (M_ComboMenuPrice.GetMenuprice()[i].ComboMenu == M_MenuNameTB.Text || M_ComboMenuPrice.GetMenuprice()[i].ComboMenu.Contains(M_MenuNameTB.Text) == true)
                        {
                            menuentercheck = true;
                            break;
                        }
                    }
                    M_MenuNameLi.SelectedIndex = i;
                }
            }catch(Exception)
            {

            }
        }
        // 메뉴 가격 텍스트박스 키 이벤트
      
        // 메뉴 리스트 클릭 이벤트
        private void M_MenuNameLi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (menuentercheck == false)
                {
                    M_MenuNameTB.Text = M_ComboMenuPrice.GetMenuprice()[M_MenuNameLi.SelectedIndex].ComboMenu;
                    M_MenuPriceTB.Text = M_ComboMenuPrice.GetMenuprice()[M_MenuNameLi.SelectedIndex].ComboMenuPrice.ToString();
                }
                else if (menuentercheck == true)
                {
                    menuentercheck = false;
                }
            }
            catch (Exception) { }
        }
        // 메뉴 저장
        private void M_MenuNameAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool check = false;
                for (int i = 0; i < M_ComboMenuPrice.GetMenuprice().Count; i++)
                {
                    if (M_ComboMenuPrice.GetMenuprice()[i].ComboMenu == M_MenuNameTB.Text)
                    {
                        check = true;
                        break;
                    }
                }
                if (check == false)
                {
                    string insertstr = "";
                    insertstr += M_MenuNameTB.Text;
                    insertstr += "&";
                    insertstr += M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;

                    int menumoney = 0;

                    if (M_MenuPriceTB.Text == "")
                    {
                        menumoney = 0;
                    }
                    else
                    {
                        menumoney = int.Parse(M_MenuPriceTB.Text);
                    }

                    PostMenuInsert(sender, e, insertstr, menumoney);
                    M_ComboMenuPrice.GetMenuprice().Add(new M_ComboMenuPrice { ComboMenu = M_MenuNameTB.Text, ComboMenuPrice = menumoney });
                    M_MenuNameLi.Items.Refresh();
                    M_MenuNameLi.SelectedIndex = M_ComboMenuPrice.GetMenuprice().Count - 1;
                }
                else
                {
                    MessageBox.Show("같은 명의 메뉴가 이미 있습니다");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("선택된 리스트가 없습니다.");
            }
        }
        // 메뉴 저장 POST
        private async void PostMenuInsert(object sender, RoutedEventArgs e, string insertstr, int menumoney)
        {
            try
            {
                var menu = new M_ComboMenuPrice()
                {
                    ComboMenu = insertstr,
                    ComboMenuPrice = menumoney
                };
                var response = await client.PostAsJsonAsync("OrderManager/menu/insert", menu);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();

            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("저장에러");
            }
        }
        // 메뉴 수정
        private void M_MenuNameUpd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string upstr = "";
                upstr += M_MenuNameTB.Text;
                upstr += "&";
                upstr += M_ComboMenuPrice.GetMenuprice()[M_MenuNameLi.SelectedIndex].ComboMenu;
                upstr += "&";
                upstr += M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;

                PostMenuUpdate(sender, e, upstr);

                M_ComboMenuPrice.GetMenuprice()[M_MenuNameLi.SelectedIndex].ComboMenu = M_MenuNameTB.Text;
                M_ComboMenuPrice.GetMenuprice()[M_MenuNameLi.SelectedIndex].ComboMenuPrice = int.Parse(M_MenuPriceTB.Text);
                M_MenuNameLi.Items.Refresh();
            }
            catch(Exception)
            {

            }
        }
        // 메뉴 수정 POST
        private async void PostMenuUpdate(object sender, RoutedEventArgs e, string upstr)
        {
            try
            {
                var menu = new M_ComboMenuPrice()
                {
                    ComboMenu = upstr,
                    ComboMenuPrice = int.Parse(M_MenuPriceTB.Text)
                };
                var response = await client.PostAsJsonAsync("OrderManager/menu/update", menu);
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
        // 메뉴 삭제

        private void M_MenuNameDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("선택하신 메뉴가 삭제 됩니다.\r계속 하시겠습니까?", "종류 삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string deletestr = "";
                deletestr += M_ComboMenuPrice.GetMenuprice()[M_MenuNameLi.SelectedIndex].ComboMenu;
                deletestr += "&";
                deletestr += M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;

                PostMenuDelete(sender, e, deletestr);
                if (M_MenuNameLi.SelectedItems.Count > 0)
                {
                    M_ComboMenuPrice.GetMenuprice().RemoveAt(M_MenuNameLi.SelectedIndex);
                    M_MenuNameTB.Text = "";
                    M_MenuPriceTB.Text = "";
                }
                else
                {
                    M_ComboMenuPrice.GetMenuprice().RemoveAt(M_ComboMenuPrice.GetMenuprice().IndexOf(new M_ComboMenuPrice() { ComboMenu = M_MenuNameTB.Text }));
                    M_MenuNameTB.Text = "";
                    M_MenuPriceTB.Text = "";
                }
            }
            M_MenuNameLi.Items.Refresh();
        }
        // 메뉴 삭제 POST
        private async void PostMenuDelete(object sender, RoutedEventArgs e, string deletestr)
        {
            try
            {
                var menu = new M_ComboMenuPrice()
                {
                    ComboMenu = deletestr,
                   ComboMenuPrice = int.Parse(M_MenuPriceTB.Text)
                };
                var response = await client.PostAsJsonAsync("OrderManager/menu/delete", menu);
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

        #endregion

        #region 옵션
        private async void GetOrderOptionData(string storemsg)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderManager/option/{0}", storemsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.
                var tests = await response.Content.ReadAsAsync<IEnumerable<M_ComboOptionPrice>>();

                M_ComboOptionPrice.GetOptionprice().Clear();
                M_OptionLi.Items.Refresh();
                if (M_PastAllLI.SelectedIndex == -1)
                {
                    M_OptionTB.Text = "";
                    M_OptionPriceTB.Text = "";
                }
                else
                {

                }

                foreach (var item in tests)
                {
                    M_ComboOptionPrice.GetOptionprice().Add(new M_ComboOptionPrice { ComboOption = item.ComboOption, ComboOptionPrice = item.ComboOptionPrice });
                }
                M_OptionLi.ItemsSource = M_ComboOptionPrice.GetOptionprice();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                // MessageBox.Show("서버 연결이 되있지 않습니다");
            }
        }
        // 메뉴 키이벤트
        private void M_OptionTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Back)
                {
                    int i;
                    for (i = 0; i < M_ComboOptionPrice.GetOptionprice().Count; i++)
                    {
                        if (M_ComboOptionPrice.GetOptionprice()[i].ComboOption == M_OptionTB.Text || M_ComboOptionPrice.GetOptionprice()[i].ComboOption.Contains(M_OptionTB.Text) == true)
                        {
                            optionentercheck = true;
                            break;
                        }
                    }
                    M_OptionLi.SelectedIndex = i;
                }
            }
            catch (Exception) { }
        }
        // 메뉴 가격 텍스트박스 키 이벤트

        // 메뉴 리스트 클릭 이벤트
        private void M_OptionLi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (optionentercheck == false)
                {
                    M_OptionTB.Text = M_ComboOptionPrice.GetOptionprice()[M_OptionLi.SelectedIndex].ComboOption;
                    M_OptionPriceTB.Text = M_ComboOptionPrice.GetOptionprice()[M_OptionLi.SelectedIndex].ComboOptionPrice.ToString();
                }
                else if (optionentercheck == true)
                {
                    optionentercheck = false;
                }
            }
            catch (Exception) { }
        }
        // 메뉴 저장
        private void M_OptionAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool check = false;
                for (int i = 0; i < M_ComboOptionPrice.GetOptionprice().Count; i++)
                {
                    if (M_ComboOptionPrice.GetOptionprice()[i].ComboOption == M_OptionTB.Text)
                    {
                        check = true;
                        break;
                    }
                }
                if (check == false)
                {
                    string insertstr = "";
                    insertstr += M_OptionTB.Text;
                    insertstr += "&";
                    insertstr += M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;

                    int optionmoney = 0;

                    if (M_OptionPriceTB.Text == "")
                    {
                        optionmoney = 0;
                    }
                    else
                    {
                        optionmoney = int.Parse(M_OptionPriceTB.Text);
                    }

                    PostOptionInsert(sender, e, insertstr, optionmoney);
                    M_ComboOptionPrice.GetOptionprice().Add(new M_ComboOptionPrice { ComboOption = M_OptionTB.Text, ComboOptionPrice = optionmoney });
                    M_OptionLi.Items.Refresh();
                    M_OptionLi.SelectedIndex = M_ComboOptionPrice.GetOptionprice().Count - 1;
                }
                else
                {
                    MessageBox.Show("같은 옵션이 이미 있습니다");
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("선택된 리스트가 없습니다.");
            }
        }
        // 메뉴 저장 POST
        private async void PostOptionInsert(object sender, RoutedEventArgs e, string insertstr, int optionmoney)
        {
            try
            {
                var option = new M_ComboOptionPrice()
                {
                    ComboOption = insertstr,
                    ComboOptionPrice = optionmoney
                };
                var response = await client.PostAsJsonAsync("OrderManager/option/insert", option);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();

            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("저장에러");
            }
        }
        // 메뉴 수정
        private void M_OptionUpd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string upstr = "";
                upstr += M_OptionTB.Text;
                upstr += "&";
                upstr += M_ComboOptionPrice.GetOptionprice()[M_OptionLi.SelectedIndex].ComboOption;
                upstr += "&";
                upstr += M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;

                PostOptionUpdate(sender, e, upstr);

                M_ComboOptionPrice.GetOptionprice()[M_OptionLi.SelectedIndex].ComboOption = M_OptionTB.Text;
                M_ComboOptionPrice.GetOptionprice()[M_OptionLi.SelectedIndex].ComboOptionPrice = int.Parse(M_OptionPriceTB.Text);
                M_OptionLi.Items.Refresh();
            }
            catch (Exception) { }
        }
        // 메뉴 수정 POST
        private async void PostOptionUpdate(object sender, RoutedEventArgs e, string upstr)
        {
            try
            {
                var option = new M_ComboOptionPrice()
                {
                    ComboOption = upstr,
                    ComboOptionPrice = int.Parse(M_OptionPriceTB.Text)
                };
                var response = await client.PostAsJsonAsync("OrderManager/option/update", option);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("수정에러");
            }
        }
        // 메뉴 삭제

        private void M_OptionDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("선택하신 옵션이 삭제 됩니다.\r계속 하시겠습니까?", "옵션 삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string deletestr = "";
                    deletestr += M_ComboOptionPrice.GetOptionprice()[M_OptionLi.SelectedIndex].ComboOption;
                    deletestr += "&";
                    deletestr += M_ComboStore.GetStore()[M_StoreNameLi.SelectedIndex].ComboStore;

                    PostOptionDelete(sender, e, deletestr);
                    if (M_OptionLi.SelectedItems.Count > 0)
                    {
                        M_ComboOptionPrice.GetOptionprice().RemoveAt(M_OptionLi.SelectedIndex);
                        M_OptionTB.Text = "";
                        M_OptionPriceTB.Text = "";
                    }
                    else
                    {
                        M_ComboOptionPrice.GetOptionprice().RemoveAt(M_ComboOptionPrice.GetOptionprice().IndexOf(new M_ComboOptionPrice() { ComboOption = M_OptionTB.Text }));
                        M_OptionTB.Text = "";
                        M_OptionPriceTB.Text = "";
                    }
                }
                M_OptionLi.Items.Refresh();
            }
            catch (Exception) { }
        }
        // 메뉴 삭제 POST
        private async void PostOptionDelete(object sender, RoutedEventArgs e, string deletestr)
        {
            try
            {
                var option = new M_ComboOptionPrice()
                {
                    ComboOption = deletestr,
                    ComboOptionPrice = int.Parse(M_OptionPriceTB.Text)
                };
                var response = await client.PostAsJsonAsync("OrderManager/option/delete", option);
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
        #endregion

        #region 과거 데이터 불러오기
        private void M_PastAllbtn_Click(object sender, RoutedEventArgs e)
        {
            string pastmsg = "";

            M_PastAllLI.SelectedItem = null;

            if (M_PastAllCB.SelectedIndex == 0) { pastmsg += "PastUserName"; }
            else if (M_PastAllCB.SelectedIndex == 1) { pastmsg += "PastKindName"; }
            else if (M_PastAllCB.SelectedIndex == 2) { pastmsg += "PastStoreName"; }
            pastmsg += "&";
            pastmsg += M_PastAllTB.Text;

            GetPastOrderData(pastmsg);

            M_FoodKindLi.SelectedItem = null;
            M_StoreNameLi.SelectedItem = null;
            M_MenuNameLi.SelectedItem = null;
            M_OptionLi.SelectedItem = null;

            M_FoodKindLi.Items.Refresh();
            M_FoodKindTB.Text = "";

            M_ComboStore.GetStore().Clear();
            M_StoreNameLi.Items.Refresh();
            M_StoreNameTB.Text = "";

            M_ComboMenuPrice.GetMenuprice().Clear();
            M_MenuNameLi.Items.Refresh();
            M_MenuNameTB.Text = "";
            M_MenuPriceTB.Text = "";

            M_ComboOptionPrice.GetOptionprice().Clear();
            M_OptionLi.Items.Refresh();
            M_OptionTB.Text = "";
            M_OptionPriceTB.Text = "";
        }
        private void M_PastAllLI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                M_StoreNameLi.Items.Refresh();
                M_StoreNameTB.Text = "";
                
                M_MenuNameLi.Items.Refresh();
                M_MenuNameTB.Text = "";
                M_MenuPriceTB.Text = "";
                
                M_OptionLi.Items.Refresh();
                M_OptionTB.Text = "";
                M_OptionPriceTB.Text = "";

               /* for (int i = 0; i < M_ComboKind.GetKind().Count; i++)
                {
                    if (M_ComboKind.GetKind()[i].ComboKind == C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastKindName || M_ComboKind.GetKind()[i].ComboKind.Contains(C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastKindName) == true)
                    {
                        throw new Exception("이미 저장되있는 데이터 입니다");
                    }
                }*/
                M_FoodKindTB.Text = C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastKindName;
                M_StoreNameTB.Text = C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastStoreName;

                if (C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastMenuName != "x")
                {
                    M_MenuNameTB.Text = C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastMenuName;
                    M_MenuNameLi.SelectedItem = null;
                }

                if (C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastOptionDes != "x")
                {
                    M_OptionTB.Text = C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastOptionDes;
                    M_OptionLi.SelectedItem = null;
                }

                SelectDefault();
            }
            catch(Exception)
            {
            }
            
        }
        private void SelectDefault()
        {
            try
            {
                int i;
                for (i = 0; i < M_ComboKind.GetKind().Count; i++)
                {
                    if (M_ComboKind.GetKind()[i].ComboKind == C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastKindName)
                    {
                        M_FoodKindLi.SelectedIndex = i;
                        break;
                    }
                } 
                for (i = 0; i < M_ComboStore.GetStore().Count; i++)
                {
                    if (M_ComboStore.GetStore()[i].ComboStore == C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastStoreName)
                    {
                        if (i != 0 || M_ComboStore.GetStore()[i].ComboStore != "")
                        { M_StoreNameLi.SelectedIndex = i; }
                        break;
                    }
                }
                for (i = 0; i < M_ComboMenuPrice.GetMenuprice().Count; i++)
                {
                    if (M_ComboMenuPrice.GetMenuprice()[i].ComboMenu == C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastMenuName)
                    {
                        M_MenuNameLi.Items.Refresh();
                        if (i != 0 || M_ComboMenuPrice.GetMenuprice()[i].ComboMenu != "")
                        { M_MenuNameLi.SelectedIndex = i; }
                        break;
                    }
                }
                for (i = 0; i < M_ComboOptionPrice.GetOptionprice().Count; i++)
                {
                    if (M_ComboOptionPrice.GetOptionprice()[i].ComboOption == C_PastOrder.GetPastlist()[M_PastAllLI.SelectedIndex].PastOptionDes)
                    {
                        M_OptionLi.Items.Refresh();
                        if (i != 0 || M_ComboOptionPrice.GetOptionprice()[i].ComboOption != "")
                        { M_OptionLi.SelectedIndex = i; }
                        break;
                    }
                        
                }
            }
            catch(Exception)
            {

            }
        }
        private async void GetPastOrderData(string pastmsg)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderManager/past/{0}",pastmsg));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<C_PastOrder>>();

                C_PastOrder.GetPastlist().Clear();
                M_PastAllTB.Text = "";

                foreach (var item in tests)
                {
                    C_PastOrder.GetPastlist().Add(new C_PastOrder {
                        PastKindName = item.PastKindName,
                        PastStoreName = item.PastStoreName,
                        PastMenuName = item.PastMenuName,
                        PastOptionDes = item.PastOptionDes,
                    });
                }
                M_PastAllLI.ItemsSource = C_PastOrder.GetPastlist();
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

        private void OnListViewItemPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (M_PastAllLI.SelectedIndex != -1)
            {
                M_PastAllLI.SelectedItem = null;
                M_FoodKindTB.Text = "";
            }
            else { }
        }
    }
}
