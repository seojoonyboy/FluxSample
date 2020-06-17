using System;
using TMPro;
using UnityEngine;

public class UserInfoView : View {
    public UserDataUI userDataUi;
    
    public override void RefreshView() {
        var userData = GameManager
            .Instance
            .UserStore()
            .GetUserData();
        
        userDataUi.userName.text = userData.nickName;
        userDataUi.description.text = userData.description;
        
        Debug.Log("Refresh User Info View");
    }
    
    [Serializable]
    public class UserDataUI {
        public TextMeshProUGUI userName;
        public TextMeshProUGUI description;
    }
}