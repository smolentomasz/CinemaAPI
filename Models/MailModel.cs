using System;
using System.Text;
using System.Security.Authentication;
using MimeKit;
using MimeKit.Text;

namespace Models
{
    public class MailModel
    {
        public static void sendMessage(ReservationInfo reservation, string messageType)
        {
            var messageToSend = new MimeMessage
            {
                Sender = new MailboxAddress("Cinema Centre", "mail@mail.pl"),
                Subject = "Reservation details",
            };

            messageToSend.From.Add(new MailboxAddress("Cinema Centre", "mail@mail.pl"));

            if(messageType.Equals("ADD")){
                StringBuilder seatStringBuilder = new StringBuilder();

                foreach(Seat seat in reservation.seats){
                    seatStringBuilder.Append("Row: " + seat.SeatRow + ", number: " + seat.SeatNumber + " ");
                }

                messageToSend.Body = new TextPart(TextFormat.Html) { 
                    Text = "Thank you for your reservation - UUID: " + reservation.uuid + " Details: <br> Movie title: " + 
                    reservation.schedule.Movie.Name + "<br> Date: " + reservation.schedule.Date + "<br> Time: " + 
                    reservation.schedule.Time + "<br> Paid: " + reservation.paid + "<br> Reserved seats: " + seatStringBuilder.ToString()};
            }
            else{
                messageToSend.Body = new TextPart(TextFormat.Html) { Text = "Reservation deleted succesfully!" };
            }

            messageToSend.To.Add(new MailboxAddress("Client",reservation.user.Email));


            using(var smtp = new MailKit.Net.Smtp.SmtpClient()){
                smtp.SslProtocols = SslProtocols.Tls;
                smtp.Connect("mail@mail.pl", 465);
                smtp.Authenticate("mail@mail.pl", "MY_SECRET_PASSWORD");
                smtp.Send(messageToSend);
            }
        }
    }
}
