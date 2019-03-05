package com.aarondunne.fypandroidapp.controller;

import com.aarondunne.fypandroidapp.model.User;
import com.aarondunne.fypandroidapp.view.IView;

public class RegisterController implements IRegisterController {
    private IView iView;

    public RegisterController(IView iView) {
        this.iView = iView;
    }

    @Override
    public void onRegister(String newFirstName, String newLastName, String newEmailID, String newUserPass, String newConfirmPass) {
        User newUser = new User(newFirstName,newLastName, newEmailID,  newUserPass, newConfirmPass);
        int errorCode = newUser.isDataValid();

        switch (errorCode) {
            case 0:     iView.onRegisterError("Please provide a valid UL email address");                       break;
            case 1:     iView.onRegisterError("Please provide a valid UL email address");                       break;
            case 2:     iView.onRegisterError("Passwords must be more then 8 characters");                      break;
            case 3:     iView.onRegisterError("Please enter your first name");                                  break;
            case 4:     iView.onRegisterError("Please enter your last name");                                   break;
            case 5:     iView.onRegisterError("Please provide a password");                                     break;
            case 6:     iView.onRegisterError("Please re-enter your password");                                 break;
            case 7:     iView.onRegisterError("Password and ConfirmPassword do not match. Please try again");   break;
            default:    iView.onRegisterSuccess("Registration Successful. Redirecting to Home screen");                                     break;
        }
    }

}
