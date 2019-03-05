package com.aarondunne.fypandroidapp.coordinator;

import com.aarondunne.fypandroidapp.model.User;
import com.aarondunne.fypandroidapp.view.ILoginView;

public class LoginCoordinator implements ILoginCoordinator {

    ILoginView loginView;
    public LoginCoordinator(ILoginView loginView) {
        this.loginView = loginView;
    }

    @Override
    public void onLogin(String emailID, String userPass) {
        User user = new User(emailID, userPass);
        int errorCode = user.isDataValid();

        switch (errorCode) {
            case 0:     loginView.onLoginError("Please provide a valid UL email address");   break;
            case 1:     loginView.onLoginError("Please provide a valid UL email address");   break;
            case 2:     loginView.onLoginError("Password must be more then 8 characters");   break;
            default:    loginView.onLoginSuccess("Login Successful");
        }

    }
}

