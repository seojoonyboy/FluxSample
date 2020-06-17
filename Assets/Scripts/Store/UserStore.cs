using Flux;

public class UserStore : Store<Actions> {
    private User user;
    public UserStore(Dispatcher<Actions> dispatcher) : base(dispatcher) {
        
    }

    protected override void _onDispatch(Actions action) {
        switch (action.type) {
            case ActionTypes.GET_USER_DATAS:
                //test code (suppose you receive web request callback)
                __GetDummyUserData();
                
                // web request (If you have web server)
                // NetworkManager.Instance.request("GET", "/user", response => {
                //     _emitChange();
                // });
                
                _emitChange();
                break;
        }
    }

    public User GetUserData() {
        return user;
    }

    private void __GetDummyUserData() {
        if(user == null) user = new User();
        user.nickName = "TestUser";
        user.description = "Description about TestUser";
    }

    public class User {
        public string nickName;
        public string description;
    }
}