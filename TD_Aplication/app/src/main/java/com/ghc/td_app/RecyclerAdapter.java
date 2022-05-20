package com.ghc.td_app;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.ghc.td_app.Model.Past_All_Model;

import java.util.ArrayList;
import java.util.List;

public class RecyclerAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {

    private List<Past_All_Model> past_list = new ArrayList<>();

    public RecyclerAdapter(List<Past_All_Model> pa_list)
    {
        past_list.clear();
        past_list = pa_list;
    }
    class ViewHolder extends RecyclerView.ViewHolder{

        private TextView pa_kind,pa_store,pa_menu,pa_option;
        public ViewHolder(View itemView){
            super(itemView);
            pa_kind=(TextView)itemView.findViewById(R.id.filed_kind_name);
            pa_store=(TextView)itemView.findViewById(R.id.filed_store_name);
            pa_menu=(TextView)itemView.findViewById(R.id.filed_menu);
            pa_option=(TextView)itemView.findViewById(R.id.filed_option);
        }
    }
    @NonNull
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View view= LayoutInflater.from(viewGroup.getContext())
                .inflate(R.layout.order_filed,viewGroup,false);
        return new RecyclerAdapter.ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerView.ViewHolder viewHolder, int i) {
        ViewHolder holder=(ViewHolder)viewHolder;
        holder.pa_kind.setText(past_list.get(i).getPastKindName());
        holder.pa_store.setText(past_list.get(i).getPastStoreName());
        holder.pa_menu.setText(past_list.get(i).getPastMenuName());
        holder.pa_option.setText(past_list.get(i).getPastOptionDes());

    }

    @Override
    public int getItemCount() {
        return  past_list.size();
    }
}
