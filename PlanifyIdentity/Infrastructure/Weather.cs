using Newtonsoft.Json;

namespace PlanifyIdentity.Infrastructure;
public class Datum
{
    [JsonProperty("app_temp")]
    public double? AppTemp { get; set; }

    [JsonProperty("aqi")]
    public int? Aqi { get; set; }

    [JsonProperty("city_name")]
    public string CityName { get; set; }

    [JsonProperty("clouds")]
    public int? Clouds { get; set; }

    [JsonProperty("country_code")]
    public string CountryCode { get; set; }

    [JsonProperty("datetime")]
    public string Datetime { get; set; }

    [JsonProperty("dewpt")]
    public double? Dewpt { get; set; }

    [JsonProperty("dhi")]
    public int? Dhi { get; set; }

    [JsonProperty("dni")]
    public int? Dni { get; set; }

    [JsonProperty("elev_angle")]
    public double? ElevAngle { get; set; }

    [JsonProperty("ghi")]
    public int? Ghi { get; set; }

    [JsonProperty("gust")]
    public object Gust { get; set; }

    [JsonProperty("h_angle")]
    public int? HAngle { get; set; }

    [JsonProperty("lat")]
    public double? Lat { get; set; }

    [JsonProperty("lon")]
    public double? Lon { get; set; }

    [JsonProperty("ob_time")]
    public string ObTime { get; set; }

    [JsonProperty("pod")]
    public string Pod { get; set; }

    [JsonProperty("precip")]
    public int? Precip { get; set; }

    [JsonProperty("pres")]
    public double? Pres { get; set; }

    [JsonProperty("rh")]
    public int? Rh { get; set; }

    [JsonProperty("slp")]
    public double? Slp { get; set; }

    [JsonProperty("snow")]
    public int? Snow { get; set; }

    [JsonProperty("solar_rad")]
    public int? SolarRad { get; set; }

    [JsonProperty("sources")]
    public List<string> Sources { get; set; }

    [JsonProperty("state_code")]
    public string StateCode { get; set; }

    [JsonProperty("station")]
    public string Station { get; set; }

    [JsonProperty("sunrise")]
    public string Sunrise { get; set; }

    [JsonProperty("sunset")]
    public string Sunset { get; set; }

    [JsonProperty("temp")]
    public double? Temp { get; set; }

    [JsonProperty("timezone")]
    public string Timezone { get; set; }

    [JsonProperty("ts")]
    public int? Ts { get; set; }

    [JsonProperty("uv")]
    public int? Uv { get; set; }

    [JsonProperty("vis")]
    public int? Vis { get; set; }

    [JsonProperty("weather")]
    public Weather Weather { get; set; }

    [JsonProperty("wind_cdir")]
    public string WindCdir { get; set; }

    [JsonProperty("wind_cdir_full")]
    public string WindCdirFull { get; set; }

    [JsonProperty("wind_dir")]
    public int? WindDir { get; set; }

    [JsonProperty("wind_spd")]
    public double? WindSpd { get; set; }
}

public class WeatherResponse
{
    [JsonProperty("count")]
    public int? Count { get; set; }

    [JsonProperty("data")]
    public List<Datum> Data { get; set; }
}

public class Weather
{
    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("code")]
    public int? Code { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }
}



