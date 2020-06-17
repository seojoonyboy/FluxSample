using UnityEngine;

public class ViewController : MonoBehaviour {
    public View view;

    public virtual void OpenPanel() {
        view.gameObject.SetActive(true);
    }

    public virtual void ClosePanel() {
        view.gameObject.SetActive(false);
    }
}