package com.aarondunne.fypandroidapp.model;

public interface IUser {
    String getFirstName();
    String getLastName();
    String getEmailID();
    String getUserPass();
    String getConfirmPass();
    int isDataValid();
}
