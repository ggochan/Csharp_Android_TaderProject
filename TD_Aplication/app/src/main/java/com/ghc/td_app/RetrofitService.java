package com.ghc.td_app;

import com.ghc.td_app.Model.One_Kind_Model;
import com.ghc.td_app.Model.One_Menu_Model;
import com.ghc.td_app.Model.One_Option_Model;
import com.ghc.td_app.Model.One_Store_Model;
import com.ghc.td_app.Model.Ordering_public_Model;
import com.ghc.td_app.Model.Past_All_Model;
import com.ghc.td_app.Model.Voting_All;
import com.ghc.td_app.Model.Voting_Content;
import com.ghc.td_app.Model.Voting_Title;
import com.ghc.td_app.Model.Voting_User;
import com.ghc.td_app.Ordering.Model_Ordering_list;
import com.ghc.td_app.TakeOrder.TakeOrder_Update_Model;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

/*@Path("") String aa << 단일
 * @GET("users/list?sort=desc") 쿼리지정
 * @Body Model model << 모델
 * @multipart
 * @Part("") RequestBody << 각각 지정
 * */
public interface RetrofitService{
    @GET("/OrderList/orderbool/{orderbool}") // main - list check
    Call<List<String>> getOrderList(@Path("orderbool") String check);

    @GET("/OrderInfo/userid/select/{idcheck}") // setting - idcheck
    Call<List<String>> getUserId(@Path("idcheck") String idcheck);

    @POST("/OrderList") // TakeOrder - 종류, 가게명 저장
    Call<List<String>> postTakeOrder(@Body TakeOrder_Update_Model tum);

    @POST("/OrderInfo") // Ordering -유저, 메뉴, 옵션 저장
    Call<List<String>> postMenuOption(@Body Ordering_public_Model opm);

    @GET("/OrderCombo/allk") // TakeOrder_Combobox - 종류 불러오기
    Call<List<One_Kind_Model>> getComboKind();

    @GET("/OrderCombo/kindname/{kindname}") // TakeOrder_Combobox - 종류 불러오기
    Call<List<One_Kind_Model>> getComboKind_Select(@Path("kindname") String kind_ck);

    @GET("/OrderCombo/storename/{kindname}") // TakeOrder_Combobox - 가게명 불러오기
    Call<List<One_Store_Model>> getComboStore(@Path("kindname") String kind_ck);

    @GET("/OrderCombo/menu/{storename}") // TakeOrder_Combobox - 메뉴 불러오기
    Call<List<One_Menu_Model>> getComboMenu(@Path("storename") String store_ck);

    @GET("/OrderCombo/option/{storename}") // TakeOrder_Combobox - 옵션 불러오기
    Call<List<One_Option_Model>> getComboOption(@Path("storename") String store_ck);

    @GET("OrderList") // Ordering - 종류, 가게명 불러오기
    Call<List<String>> getTakeOrder();

    @GET("OrderInfo") // Ordering - 주문내역 불러오기
    Call<List<Model_Ordering_list>> getTakeOrderList();

    @GET("OrderList/Count") // Ordering - 마지막 달성
    Call<List<String>> GetCountEnd();

    @GET("OrderInfo/reseipt") // Ordering - 마지막 달성
    Call<List<String>> GettakeorderEnd();

    @GET("/OrderInfo/usermenu/{userid}") // Ordering - 현재 유저 주문내역 불러오기(Update 용)
    Call<List<String>> getUserNow(@Path("userid") String user_now);

    @POST("/OrderInfo/update/usermenu") // Ordering -유저, 메뉴, 옵션 저장
    Call<List<String>> postMenuUpdate(@Body Ordering_public_Model opm);

    @GET("/OrderInfo/orderlistall") // Main - 현재 주문 내역 불러오기
    Call<List<String>> getOrderlistNow();

    @GET("/OrderManager/past/{objectname}") // Main - 과거 주문 내역 불러오기
    Call<List<Past_All_Model>> getPastList(@Path("objectname") String pa_str);

    @GET("/Voting/votingbool") // Main - 투표 존재 여부
    Call<List<String>> getVotingbool();

    @POST("/Voting/insert/title") // 투표함 - 타이틀 저장
    Call<List<String>> postVotingTitle(@Body Voting_Title vt);

    @GET("/Voting/select/title") // 투표함 - 타이틀 출력
    Call<List<Voting_Title>> GetVotingTitle();

    @POST("/Voting/insert/content") // 투표함 - 항목 저장
    Call<List<String>> postVotingContent(@Body Voting_Content vc);

    @GET("/Voting/select/content") // 투표함 - 항목 출력
    Call<List<Voting_Content>> GetVotingContent();

    @GET("/Voting/select/userall") // 투표함 - 유저 출력
    Call<List<Voting_User>> GetVotingUser();

    @GET("/Voting/select/{userid}") // 투표함 - 유저 존재여부 확인
    Call<List<String>> GetVotingUserCheck(@Path("userid") String userid);

    @POST("/Voting/insert/user") // 투표함 - 유저 저장
    Call<List<String>> postVotingUser(@Body Voting_User vu);

    @GET("/Voting/select/user/{contentnum}") // 투표함 - 해당 항목 유저 정보 불러오기
    Call<List<String>> GetVotingConNumUser(@Path("contentnum") int contentnum);

    @POST("/Voting/update/userid") // 투표함 - 유저 투표 수정
    Call<List<String>> postVotingUpdateUser(@Body Voting_User vu);

    @GET("/Voting/voting/dbinsert") // 투표함 - 투표 내역 저장
    Call<List<String>> GetVotingListInsert();

    @GET("/Voting/select/all/{userid}") // 투표함 - 해당 항목 유저 정보 불러오기
    Call<List<Voting_All>> GetVotingAllPrint(@Path("userid") String userid);

}