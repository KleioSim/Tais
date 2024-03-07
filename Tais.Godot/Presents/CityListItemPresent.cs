﻿using Tais.Interfaces;

public partial class CityListItemPresent : PresentControl<CityListItem, ISession>
{
    protected override void Initialize(CityListItem view, ISession model)
    {

    }

    protected override void Update(CityListItem view, ISession model)
    {
        var cityObj = view.Id as ICity;

        view.CityName.Text = cityObj.Name;
        view.PopCount.Text = cityObj.PopCount.Current.ToString();
        view.IsControl.Text = cityObj.IsOwned.ToString();
    }
}