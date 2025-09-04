using UnityEngine;

public class Oursin : Fish
{
    public override void CollectFish(PlayerEnum collector)
    {
        base.CollectFish(collector);
    }

    public override void InitFish(SeaDataSO seaDataSo)
    {
        base.InitFish(seaDataSo);

        _fishLifeTime = seaDataSo.OursinLifeTime;
        Score = seaDataSo.OursinScore;
    }
}
