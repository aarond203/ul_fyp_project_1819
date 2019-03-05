package com.aarondunne.fypandroidapp.model;

import android.text.TextUtils;
import android.util.Patterns;

public class User implements IUser {
    private String  firstName, lastName, emailID, userPass, confirmPass;

    public User(String emailID, String userPass) {
        this.emailID = emailID;
        this.userPass = userPass;
    }
    public User(String firstName, String lastName, String emailID, String userPass, String confirmPass) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.emailID = emailID;
        this.userPass = userPass;
        this.confirmPass = confirmPass;
    }

    @Override
    public String getFirstName() {
        return firstName;
    }

    @Override
    public String getLastName() {
        return lastName;
    }

    @Override
    public String getConfirmPass() {
        return confirmPass;
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
        if      (TextUtils.isEmpty(getEmailID()))
            return 0;
        else if (!Patterns.EMAIL_ADDRESS.matcher(getEmailID()).matches())
            return 1;
        else if (getUserPass().length() < 8 || getConfirmPass().length() < 8)
            return 2;
        else if (TextUtils.isEmpty(getFirstName()))
            return 3;
        else if (TextUtils.isEmpty(getLastName()))
            return 4;
        else if (TextUtils.isEmpty(getUserPass()))
            return 5;
        else if (TextUtils.isEmpty(getConfirmPass()))
            return 6;
        else if (!getUserPass().equals(getConfirmPass()))
            return 7;
        else
            return -1;
    }
}
