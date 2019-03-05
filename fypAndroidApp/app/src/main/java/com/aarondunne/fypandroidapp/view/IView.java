package com.aarondunne.fypandroidapp.view;

public interface IView {
    void onLoginSuccess(String msg);
    void onLoginError(String msg);
    void onRegisterSuccess(String msg);
    void onRegisterError(String msg);
}
