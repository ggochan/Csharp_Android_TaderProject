package com.ghc.td_app;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.ghc.td_app.Model.Voting_All;

import java.util.ArrayList;
import java.util.List;

public class RecyclerAdapter2 extends RecyclerView.Adapter<RecyclerView.ViewHolder> {

    private List<Voting_All> voting_list = new ArrayList<>();

    public RecyclerAdapter2(List<Voting_All> vo_list)
    {
        voting_list.clear();

        voting_list= vo_list;

    }
    class ViewHolder extends RecyclerView.ViewHolder{

        private TextView vo_title,vo_day,vo_memo,vo_content,vo_count,vo_allcount,vo_user;
        public ViewHolder(View itemView){
            super(itemView);
            vo_title=(TextView)itemView.findViewById(R.id.voting_field_title);
            vo_day=(TextView)itemView.findViewById(R.id.voting_field_day);
            vo_memo=(TextView)itemView.findViewById(R.id.voting_field_memo);
            vo_content=(TextView)itemView.findViewById(R.id.voting_field_content);
            vo_count=(TextView)itemView.findViewById(R.id.voting_field_count);
            vo_allcount=(TextView)itemView.findViewById(R.id.voting_field_allcount);
            vo_user=(TextView)itemView.findViewById(R.id.voting_field_user);
        }
    }
    @NonNull
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View view= LayoutInflater.from(viewGroup.getContext())
                .inflate(R.layout.votingbox_filed,viewGroup,false);
        return new RecyclerAdapter2.ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerView.ViewHolder viewHolder, int i) {
        ViewHolder holder=(ViewHolder)viewHolder;
        holder.vo_title.setText(voting_list.get(i).getAlltitle());
        holder.vo_day.setText(voting_list.get(i).getAllday());
        holder.vo_memo.setText(voting_list.get(i).getAllmemo());
        holder.vo_content.setText(voting_list.get(i).getAllcontent().replace("&%&","\n"));
        String temp9 = voting_list.get(i).getAllcount().replace("&%&"," 명\n") + " 명";
        holder.vo_count.setText(temp9);
        String temp10 = "투표 인원 수 : "+String.valueOf(voting_list.get(i).getAllcountAll()) + "명";
        holder.vo_allcount.setText(temp10);
        holder.vo_user.setText("투표자 : " + voting_list.get(i).getAlluser());

    }

    @Override
    public int getItemCount() {
        return  voting_list.size();
    }
}
