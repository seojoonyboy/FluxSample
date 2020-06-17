using Flux;

public class GameManager : Singleton<GameManager> {
    private QueueDispatcher<Actions> dispather;
    private UserStore _userStore;
    
    private void Awake() {
        dispather = new QueueDispatcher<Actions>();
        _userStore = new UserStore(dispather);
    }

    public QueueDispatcher<Actions> Dispatcher() {
        return dispather;
    }

    public UserStore UserStore() {
        return _userStore;
    }
}