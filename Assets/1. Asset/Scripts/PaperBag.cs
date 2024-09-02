using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBag : MonoBehaviour
{
    public Transform openBag;
    public Transform closeBag;
    public Animator ani;

    #region 애니메이션 string
    string open;
    string close;
    private void AnimString()
    {
        open = "Open";
        close = "Close";
    }
    #endregion region
    private void Start()
    {
        AnimString();   
    }

    private void OnEnable()
    {
        closeBag.gameObject.SetActive(false);
        openBag.gameObject.SetActive(true);
    }
    public void CloseBag()
    {
        closeBag.gameObject.SetActive(true);
        openBag.gameObject.SetActive(false);
        ani.SetTrigger(close);
    }
    public void OpenBag()
    {
        closeBag.gameObject.SetActive(false);
        openBag.gameObject.SetActive(true);
        ani.SetTrigger(open);
    }
}
