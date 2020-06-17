public class UserInfoController : ViewController {
    private GameManager gm;
    private void Start() {
        gm = GameManager.Instance;
        gm
            .UserStore()
            .addListener(OnUserStoreChanged);
    }

    private void OnUserStoreChanged() {
        view.RefreshView();
    }

    public override void OpenPanel() {
        base.OpenPanel();
    }

    public override void ClosePanel() {
        base.ClosePanel();
    }

    public void RefreshUserData() {
        var action = Action.ActionCreator.createAction(ActionTypes.GET_USER_DATAS);
        gm
            .Dispatcher()
            .dispatch(action);
    }
}