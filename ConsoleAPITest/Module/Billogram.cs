using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAPITest.Module
{
    public class BillogramResponse
    {
        //public string Id { get; set; }
        public string status { get; set; }
        public Billogram data { get; set; }
    }

    /// <summary>
    /// Fullständig beskrivning av ett Billogram 
    /// Används som responsobjekt vid GET av billogram
    /// </summary>
    public class Billogram
    {
        public string Id { get; set; }
        public string Invoice_no { get; set; }
        public string Ocr_number { get; set; }
        public string Creditor_unique_value { get; set; }
        public Customer customer { get; set; }
        public Item[] Items { get; set; }
        public DateTime Invoice_date { get; set; }
        public DateTime Due_date { get; set; }
        public int Due_days { get; set; }
        public DateTime? Respite_date { get; set; } 
        public int Invoice_fee { get; set; }
        public int Invoice_fee_vat { get; set; }
        //public int? reminder_fee { get; set; }
        public decimal Interest_rate { get; set; }
        public decimal Interest_fee { get; set; }
        public string Currency { get; set; }
        public Detailed_sum Detailed_sums { get; set; }
        public Info Info { get; set; }
        //public Delivery_method delivery_method { get; set; }
        public string State { get; set; }
        public string Url { get; set; }
        public string Recipient_url { get; set; }
        //public Flags flags { get; set; }
        public Events[] Events { get; set; }
        public decimal Remaining_sum { get; set; }
        public decimal Total_sum { get; set; }
        public decimal? Rounding_value { get; set; }
        public bool Automatic_reminders { get; set; }
        //public string[] automatic_reminders_settings { get; set; }
        public int Reminder_count { get; set; }
        // read only objects
        public bool Show_item_gross_prices { get; set; }
        public On_success on_success { get; set; }
    }
    /// <summary>
    /// En delbeskrivning av ett billogram för att skapa ett billogram
    /// Detta för att inte skicka på för många fält vid skapande av ett billogram
    ///     där det känns som att ett fullständigt billogram skapar problem
    /// </summary>
    public class BillogramCreate
    {
        public CustomerMini customer { get; set; }
        public ItemMini[] items { get; set; }
        public Info info { get; set; }
        public On_success on_success { get; set; }
    }

    /// <summary>
    /// Beskrivning av Customer för create av ett billogram.
    /// Begränsat antal attribute.
    /// </summary>
    public class CustomerMini
    {
        public int? customer_no { get; set; }
    }
    /// <summary>
    /// Beskrivning av Detailed_sum  ett sub-class i billogram
    /// </summary>
    public class Detailed_sum
    {
        public decimal credited_sum { get; set; }
        public decimal net_sum { get; set; }
        public decimal gross_sum { get; set; }
        public decimal invoice_fee_vat { get; set; }
        public decimal paid_sum { get; set; }
        public decimal interest_fee { get; set; }
        public decimal vat_sum { get; set; }
        public decimal reminder_fee { get; set; }
        public decimal invoice_fee { get; set; }
        public decimal rounding { get; set; }
        public Regional_sweden regional_sweden { get; set; }
    }
    /// <summary>
    /// Beskrivning av fakturarad i ett billogram
    /// </summary>
    public class Item
    {
        public decimal count { get; set; }
        public string item_no { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public Regional_sweden regional_sweden { get; set; }
        public decimal price { get; set; }
        public decimal discount { get; set; }
        public string unit { get; set; }
        public Bookkeeping bookkeeping { get; set; }
        public int vat { get; set; }
    }

    /// <summary>
    /// Beskrivning av Item för create av ett billogram.
    /// Begränsat antal attribute
    /// </summary>
    public class ItemMini
    {
        public decimal count { get; set; }
        public string item_no { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public int vat { get; set; }
    }
    /// <summary>
    /// Beskrivning av bokföringsuppgifter i ett billogram
    /// </summary>
    public class Bookkeeping
    {
        public string vat_account { get; set; }
        public string income_account { get; set; }
    }
    /// <summary>
    /// Beskrivning av Svenska reogional uppgifter som kan finnas i ett billogram
    /// </summary>
    public class Regional_sweden
    {
        public decimal Rotavdrag_sum { get; set; }

        //"electricity_collection": Depricate
        public DateTime autogiro_payment_date { get; set; }
        public string autogiro_status { get; set; }
        public string rotavdrag_housing_association_org_no { get; set; }
        public string autogiro_full_status { get; set; }
        public Edi edi { get; set; }
        public decimal efaktura_requested_amount { get; set; }
        public string rotavdrag_description { get; set; }
        public decimal autogiro_total_sum { get; set; }
        public string efaktura_recipient_bank_code { get; set; }
        public string rotavdrag_apartment_designation { get; set; }
        public string rotavdrag_description_of_property { get; set; }
        public string efaktura_recipient_bank_id { get; set; }
        public Boolean rotavdrag { get; set; }
        public string efaktura_recipient_type { get; set; }
        public string efaktura_recipient_bank_name { get; set; }
        public string autogiro_betalarnummer { get; set; }
        public Boolean reversed_vat { get; set; }
        public string efaktura_recipient_identifier { get; set; }
        public string efaktura_recipient_id_number { get; set; }

        public class Edi
        {
            public string Operator { get; set; }
            public string subtype { get; set; }
            public string note { get; set; }
            public string electronic_id { get; set; }
            public string reference { get; set; }
        }
    }

    public class Events
    {
        public string type { get; set; }
        public DateTime created_at { get; set; }
        public Guid event_uuid { get; set; }
        public Eventdata eventdata { get; set; }
    }
    public class Eventdata
    {
        public string letter_id { get; set; }
        public decimal total_sum { get; set; }
        public bool scanning_central { get; set; }
        public decimal remaining_sum { get; set; }
        public string delivery_method { get; set; }
        public string invoice_no { get; set; }
    }
    public class Info
    {
        public string reference_number { get; set; }
        public string our_reference { get; set; }
        public string order_no { get; set; }
        public string order_date { get; set; }
        public string shipping_date { get; set; }
        public string message { get; set; }
        public DateTime? delivery_date { get; set; }
        public string your_reference { get; set; }
    }
    public class On_success
    {
        public string command { get; set; }
        public string method { get; set; }
    }

    public enum eBillogramStates
    {
        /// <summary>A billogram that has been created, but not yet sent or sold.</summary>
        Unattested,
        /// <summary>A billogram that is being sent right now (will only be visible in callbacks).</summary>
        Sending,
        /// <summary>A sent billogram that hasn't been paid yet.</summary>
        Unpaid,
        /// <summary>An ended billogram with a collection request that has been ended.</summary>
        CollectorEnded,
        /// <summary>A billogram that is being sent for collection but has not yet been verified by the collection partner.</summary>
        Collecting,
        /// <summary>A billogram that has been sent for collection.</summary>
        Collection,
        /// <summary>A billogram that is being sold but has not yet been approved by the factoring partner.</summary>
        Factoring,
        /// <summary>A sold billogram. This is an ended state.</summary>
        Sold,
        /// <summary>An ended billogram. This state will only happen if the billogram was sent with a zero sum amount.</summary>
        Ended,
        /// <summary>An ended billogram that has been paid.</summary>
        Paid,
        /// <summary>An ended billogram that has been credited.</summary>
        Credited,
        /// <summary>A billogram that was sent for factoring but the sale request was denied by the factoring partner.</summary>
        FactoringDenied,
        /// <summary>A billogram that has received a payment.</summary>
        PartlyPaid,
        /// <summary>A billogram that has past it's due date and not yet received a payment.</summary>
        Overdue,
        /// <summary>A billogram that has been reminded once.</summary>
        Reminder_1,
        /// <summary>A billogram that has been reminded twice.</summary>
        Reminder_2,
        /// <summary>A billogram that has been reminded thrice.</summary>
        Reminder_3,
        /// <summary>A billogram that has been reminded more than 3 times.</summary>
        Reminder_Many,
        /// <summary>A deleted billogram. This state will only happen if the billogram has been deleted and it will only be shown in a potential callback.</summary>
        Deleted

    }
}
