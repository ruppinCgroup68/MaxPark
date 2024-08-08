using projMaxPark.BL;
using projMaxPark.DAL;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

public class NotificationService
{
    // This method sends a notification to the specified user
    public string SendNotification(int userId, string title, string body)
    {
        // Assuming you have a method to retrieve the user's push notification token by userId
        DBservicesUser servicesUser = new DBservicesUser();
        User user = new User();
        string userPushToken = user.GetPushTokenByUserId(userId); // Use null conditional operator

        if (string.IsNullOrEmpty(userPushToken))
        {
            return "No push token found for user.";
        }

        // Create the request using the URL for sending push notifications
        WebRequest request = WebRequest.Create("https://exp.host/--/api/v2/push/send");

        // Set the Method property of the request to POST
        request.Method = "POST";

        // Create the POST data object
        var objectToSend = new
        {
            to = userPushToken,
            title = title,
            body = body
        };

        // Serialize the object to JSON
        string postData = JsonSerializer.Serialize(objectToSend);
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        // Set the ContentType property of the WebRequest
        request.ContentType = "application/json";

        // Set the ContentLength property of the WebRequest
        request.ContentLength = byteArray.Length;

        try
        {
            // Get the request stream
            using (Stream dataStream = request.GetRequestStream())
            {
                // Write the data to the request stream
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            // Get the response
            string returnStatus;
            string responseFromServer;
            using (WebResponse response = request.GetResponse())
            {
                returnStatus = ((HttpWebResponse)response).StatusDescription;

                // Get the stream containing content returned by the server
                using (Stream dataStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    // Read the content
                    responseFromServer = reader.ReadToEnd();
                }
            }

            return "Success: " + responseFromServer + ", Status: " + returnStatus;
        }
        catch (WebException ex)
        {
            // Log the exception and the response if available
            using (var errorResponse = (HttpWebResponse)ex.Response)
            {
                string errorMessage;
                using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                {
                    errorMessage = reader.ReadToEnd();
                }
                return $"Error: {errorMessage}, Status: {errorResponse.StatusCode}";
            }
        }
        catch (Exception ex)
        {
            // Handle other potential exceptions
            return $"Unexpected error: {ex.Message}";
        }
    }
}
