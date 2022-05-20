package com.ghc.td_app.Model;

import com.google.gson.annotations.SerializedName;

public class Past_All_Model {

    @SerializedName("pastKindName")
    private String PastKindName;

    @SerializedName("pastStoreName")
    private String PastStoreName;

    @SerializedName("pastMenuName")
    private String PastMenuName;

    @SerializedName("pastOptionDes")
    private String PastOptionDes;

    public Past_All_Model(String PastKindName, String PastStoreName, String PastMenuName, String PastOptionDes) {
        this.PastKindName=PastKindName;
        this.PastStoreName=PastStoreName;
        this.PastMenuName=PastMenuName;
        this.PastOptionDes=PastOptionDes;
    }
    public String getPastKindName() {
        return PastKindName;
    }

    public void setPastKindName(String pastKindName) {
        PastKindName = pastKindName;
    }

    public String getPastStoreName() {
        return PastStoreName;
    }

    public void setPastStoreName(String pastStoreName) {
        PastStoreName = pastStoreName;
    }

    public String getPastMenuName() {
        return PastMenuName;
    }

    public void setPastMenuName(String pastMenuName) {
        PastMenuName = pastMenuName;
    }

    public String getPastOptionDes() {
        return PastOptionDes;
    }

    public void setPastOptionDes(String pastOptionDes) {
        PastOptionDes = pastOptionDes;
    }

}
