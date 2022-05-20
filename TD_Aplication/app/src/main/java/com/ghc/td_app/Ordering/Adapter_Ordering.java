package com.ghc.td_app.Ordering;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.ghc.td_app.Ordering.Model_Ordering;
import com.ghc.td_app.R;

import java.util.ArrayList;

public class Adapter_Ordering extends BaseAdapter {
    public ArrayList<Model_Ordering> orderinglist;
    Activity activity;

    public Adapter_Ordering(Activity activity, ArrayList<Model_Ordering> orderinglist) {
        super();
        this.activity = activity;
        this.orderinglist = orderinglist;
    }

    @Override
    public int getCount() {
        return orderinglist.size();
    }

    @Override
    public Object getItem(int position) {
        return orderinglist.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    private class ViewHolder {
        TextView anum;
        TextView amenu;
        TextView amenucount;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        ViewHolder holder;
        LayoutInflater inflater = activity.getLayoutInflater();

        if (convertView == null) {
            convertView = inflater.inflate(R.layout.listview_row, null);
            holder = new ViewHolder();
            holder.anum = (TextView) convertView.findViewById(R.id.adap_num);
            holder.amenu = (TextView) convertView.findViewById(R.id.adap_menu);
            holder.amenucount = (TextView) convertView.findViewById(R.id.adap_menu_count);

            convertView.setTag(holder);
        } else {
            holder = (ViewHolder) convertView.getTag();
        }

        Model_Ordering item = orderinglist.get(position);
        holder.anum.setText(item.getNum().toString());
        holder.amenu.setText(item.getMenu().toString());
        holder.amenucount.setText(item.getMenu_count().toString());

        return convertView;
    }
}