
public class Action {
    public static class ActionCreator {
        public static Actions createAction(ActionTypes _type) {
            Actions _return = null;
            switch (_type) {
                case ActionTypes.GET_USER_DATAS:
                    _return = new GetUserAction(); 
                    break;
            }
            _return.type = _type;
            return _return;
        }
    }
    
    public class GetUserAction : Actions { }
}

public enum ActionTypes {
    GET_USER_DATAS
}

public class Actions{
    public ActionTypes type;
}