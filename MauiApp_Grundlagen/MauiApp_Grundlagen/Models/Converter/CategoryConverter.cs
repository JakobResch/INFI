using Onlineshop.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp_Grundlagen.Models.Converter
{

    //falls die Datentypen beim Databinding nicht zusammenpassen, muss ein Konverter
    //programmiert werden (ist eine Klasse - diese Klasse muss als Interfae IValueConverter implementiert)

    //bei uns: das Property SelectedIndex von Picker ist vom Datentyp int
    //das Property Category von CreateArticleViewModel (VM-Klasse) ist vom 
    //Typ Category (enum)
    //  -> deshalb muss eine Konvertierung durchgeführt werden
    public class CategoryConverter : IValueConverter
    {

        //Convert: konvertiert vom Property das VM-Klasse (Category) nach
        // SelectedIndex (int) von Picker

        //unser Bsp.: Category -> int
        public object Convert(object value, Type tartgetType, object parameter, CultureInfo culuture)
        {
            if (value != null)
            {
                return (int)(Category)value;
            }
            return (int)Category.notSpecified;
        }

        //umgekehrte Richtung
        //unser Bsp.: int -> Category
        public object ConvertBack(object value, Type tartgetType, object parameter, CultureInfo culuture)
        {
            if (value != null)
            {
                return (Category)(int)value;
            }
            return Category.notSpecified;

        }
    }
}

