package com.aarondunne.fypandroidapp.controller;

import com.aarondunne.fypandroidapp.model.User;
import com.aarondunne.fypandroidapp.view.IView;

public class LoginController implements ILoginController {
    private IView iView;

    public LoginController(IView iView) {
        this.iView = iView;
    }

    @Override
    public void onLogin(String emailID, String userPass) {
        User user = new User(emailID, userPass);
        int errorCode = user.isDataValid();

        switch (errorCode) {
            case 0:     iView.onLoginError("Please provide a valid UL email address");   break;
            case 1:     iView.onLoginError("Please provide a valid UL email address");   break;
            case 2:     iView.onLoginError("Password must be more then 8 characters");   break;
            default:    iView.onLoginSuccess("Login Successful");                        break;
        }

    }
}

