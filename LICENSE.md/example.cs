public class InvoiceRequest{
      public invoice getInvoice(){
            var url = "http://www.sorgulama.com";
            var resource = "api/migrasyon/ReverseCompany?stage={profileName}&customerID={customerId}";
            resource = "api/migrasyon/TransferCrm?CRMGuid={accountId}&Stage={stage}&UserDisable={isUserDisabled}&noHistory={isMigrateHistory}";

            const int delayInMinutes = 10;


            var client = new RestClient(url);
            var request = new RestRequest(resource, Method.POST);
            request.AddUrlSegment("accountId", model.AccountId.ToString());
            request.AddUrlSegment("stage", HttpClient.ConfigurationManager.Configuration.Entity.ProfileName);
            request.AddUrlSegment("isUserDisabled", model.IsUserDisabled.ToString());
            request.AddUrlSegment("isMigrateHistory", (!model.IsMigrateHistory).ToString());
            request.Timeout = 60 * 1000 * delayInMinutes;

            client.AddHandler("application/json", new HttpClient.RestDataAccess.Deserializer.RestSharpJsonDeserializer());

            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");


            //var response = client.Execute(request);
            //var result = JsonConvert.DeserializeObject<MigrationResponseDto>(response.Content);

            var response = client.Execute<invoie>(request);
            var result = response.Data;


            if (result == null || !result.Status || result.CompanyId == null) {
            throw new System.Exception(response.Content);
            }

            return result;
      }
}





public class RestSharpJsonDeserializer : IDeserializer {
  public string RootElement { get; set; }
  public string Namespace { get; set; }
  public string DateFormat { get; set; }
  public CultureInfo Culture { get; set; }




  public RestSharpJsonDeserializer() {
      Culture = CultureInfo.InvariantCulture;
  }




  public T Deserialize<T>(IRestResponse response) {
      var deserializedObject = JsonConvert.DeserializeObject<T>(response.Content);
      return deserializedObject;
  }


  public static T DeserializeContent<T>(IRestResponse response) {
      var obj = new RestSharpJsonDeserializer();
      var result = obj.Deserialize<T>(response);


      return result;
  }
}
