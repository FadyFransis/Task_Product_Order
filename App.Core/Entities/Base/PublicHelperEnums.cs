using System;

namespace App.Core.Entities.Base
{
    [Flags]
    public enum RecordStatus : short
    {
        Enabled = 0,
        Disabled = 1,
        Deleted = 2,
        Blocked = 3,
        ClientCancel = 4,
        AdminCancel = 5
    }

    [Flags]
    public enum OrderStatus : short
    {
        Submitted = 0,
        Confirmed = 1,
        Ontrack = 2,
        Completed = 3,
        Canceled = 4
    }

    [Flags]
    public enum Payment : short
    {
        Cash = 1,
        CreditCard = 2,
    }

    
    [Flags]
    public enum EmailTemplateKeys : short
    {
        AccountCreated = 1,
        ForgetPassword = 2,
        OrderSubmitted = 3,
        OrderChangeStatus = 4,
        OrderSubmittedForAdmin = 5,
    }


    public enum AppSettingKey : short
    {
        AboutUs = 1,
        Terms = 2,
        Privacy = 3,
        Phone1 = 4,
        Phone2 = 5,
        Email = 6,
        Address = 7,
        Facebook = 8,
        Twitter = 9,
        Instagram = 10,
        Youtube = 11,
        LinkedIn = 12,
        WorkingHours = 13,
        AddressUrl = 14,
    }
    public enum DiscountType : short
    {
        Amount = 1,
        Percentage = 2
    }
    [Flags]
    public enum LoginResult : short
    {
        Success = 0,
        InvalidCredentials = 1,
        InActive = 2,
        InvalidData=3,
        ErrorOccured=4
    } 
    [Flags]
    public enum ResponseResult : short
    {
        Success = 0,
        InvalidCredentials = 1,
        InActive = 2,
        InvalidDate=3,
        ErrorOccured=4
    }
    [Flags]
    public enum ActivateResult : short
    {
        Success = 0,
        InvalidCredentials = 1,
        InActive = 2,
        InvalidDate = 3,
        ErrorOccured = 4
    }
}
