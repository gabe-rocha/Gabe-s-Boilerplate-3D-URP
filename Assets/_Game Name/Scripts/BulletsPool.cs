public class BulletsPool : GameObjectPooler
{
    private void Start()
    {
        GameManager.Instance.bulletsPool = this;

        foreach (var obj in _listOfReadyObjects)
        {
            //Bullet bullet = obj.GetComponent<Bullet>();
            //bullet.SetPool(this);
        }
    }
}
