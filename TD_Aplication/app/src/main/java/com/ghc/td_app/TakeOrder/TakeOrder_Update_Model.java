package com.ghc.td_app.TakeOrder;

import com.google.gson.annotations.SerializedName;

public class TakeOrder_Update_Model {
    @SerializedName("KindName")
    private String KindName;

    @SerializedName("StoreName")
    private String StoreName;

    @SerializedName("Count")
    private int Count;

    public TakeOrder_Update_Model(String KindName, String StoreName, int Count) {
        this.KindName=KindName;
        this.StoreName=StoreName;
        this.Count=Count;
    }

    public String getKindName() {
        return KindName;
    }

    public void setKindName(String kindName) {
        KindName = kindName;
    }

    public String getStoreName() {
        return StoreName;
    }

    public void setStoreName(String storeName) {
        StoreName = storeName;
    }

    public int getCount() {
        return Count;
    }

    public void setCount(int count) {
        Count = count;
    }
}
