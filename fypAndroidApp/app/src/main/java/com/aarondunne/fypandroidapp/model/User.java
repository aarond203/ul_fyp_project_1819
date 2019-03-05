package com.aarondunne.fypandroidapp.model;

import android.text.TextUtils;
import android.util.Patterns;

public class User implements IUser {
    private String emailID, userPass;

    public User(String emailID, String userPass) {
        this.emailID = emailID;
        this.userPass = userPass;
    }

    @Override
    public String getEmailID() {
        return emailID;
    }

    @Override
    public String getUserPass() {
        return userPass;
    }

    public int isDataValid() {

        if (TextUtils.isEmpty(getEmailID()))
            return 0;
        else if (!Patterns.EMAIL_ADDRESS.matcher(getEmailID()).matches())
            return 1;
        else if (getUserPass().length() < 8)
            return 2;
        else
            return -1;
    }
}
