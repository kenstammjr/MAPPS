using System;


namespace MAPPS{

    public static class ADProperty {

        // Baseline User Attributes (2.2)
        public const String LAST_NAME = "sn";
        public const String FIRST_NAME = "givenName";
        public const String INITIALS = "initials";
        public const String GENERATION_QUALIFIER = "generationQualifier";
        public const String RANK_SALUTATION = "personalTitle";                         
        public const String DOD_COMPONENT = "company";                                 
        public const String DOD_SUB_COMPONENT = "department";                           
        public const String POSITION_TITLE = "title";                                   
        public const String EMPLOYEE_IDENTIFICATION = "employeeID";                     
        public const String MAIL = "mail";                                              
        public const String SMART_CARD_LOGON_EXEMPTION_CODE = "extensionAttribute3";    
        public const String CITIZENSHIP_STATUS = "extensionAttribute6";                
        public const String CITIZENSHIP_COUNTRY_CODE = "extensionAttribute7";          
        public const String PERSONNEL_CATEGORY_CODE = "employeeType";                   
        public const String DISPLAY_NAME = "displayName";                               
        public const String EMAIL_ENCRYPTION_CERTIFICATE = "userCertificate";          
        public const String BUSINESS_PHONE_COMMERCIAL = "otherTelephone";              
        public const String MOBILE_PHONE = "mobile";                                   
        public const String BUSINESS_PHONE_DSN = "telephoneNumber";
        public const String BUSINESS_FAX_COMMERCIAL = "facsimileTelephoneNumber";
        public const String BUSINESS_PHONE_SECURE = "primaryInternationalISDNNumber";
        public const String BUSINESS_FAX_SECURE_DSN = "otherFacsimileTelephoneNumber";
        public const String MOBILE_PHONE_SECURE = "otherMobile";
        public const String BUSINESS_ADDRESS_STREET = "streetAddress";                               
        public const String BUSINESS_ADDRESS_CITY = "l";                               
        public const String BUSINESS_ADDRESS_STATE = "st";                             
        public const String BUSINESS_ADDRESS_POSTAL_CODE = "postalCode";              
        public const String BUSINESS_ADDRESS_COUNTRY_CODE = "c";                     
        public const String DO_NOT_PUBLISH = "msExchHideFromAddressLists";           

        // Specific User Attributes (2.3)
        public const String ORGANIZATION_NAME = "o";                                  
        public const String OFFICE_SYMBOL = "physicalDeliveryOfficeName";              
        public const String BLDG_ROOM_NUMBER = "roomNumber";                            
        public const String USERPRINCIPALNAME = "userPrincipalName";                   

        // Additional Attributes    
        public const String OBJECT_CLASS = "objectClass";
        public const String CONTAINER_NAME = "cn";
        public const String COMMON_NAME = "cn";
        public const String CN = "cn";
        public const String DISTINGUISHED_NAME = "distinguishedName";
        public const String INSTANCE_TYPE = "instanceType";
        public const String WHEN_CREATED = "whenCreated";
        public const String WHEN_CHANGED = "whenChanged";
        public const String USN_CREATED = "uSNCreated";
        public const String USN_CHANGED = "uSNChanged";
        public const String MEMBER_OF = "memberOf";
        public const String COUNTRY = "co";
        public const String PROXY_ADDRESSES = "proxyAddresses";
        public const String DIRECT_REPORTS = "directReports";
        public const String NAME = "name";
        public const String OBJECT_GUID = "objectGUID";
        public const String USER_ACCOUNT_CONTROL = "userAccountControl";
        public const String BAD_PWD_COUNT = "badPwdCount";
        public const String CODE_PAGE = "codePage";
        public const String COUNTRY_CODE = "countryCode";
        public const String BAD_PASSWORD_TIME = "badPasswordTime";
        public const String LAST_LOGOFF = "lastLogoff";
        public const String LAST_LOGON = "lastLogon";
        public const String PWD_LAST_SET = "pwdLastSet";
        public const String PRIMARY_GROUP_ID = "primaryGroupID";
        public const String OBJECT_SID = "objectSid";
        public const String ADMIN_COUNT = "adminCount";
        public const String ACCOUNT_EXPIRES = "accountExpires";
        public const String LOGON_COUNT = "logonCount";
        public const String LOGIN_NAME = "sAMAccountName";
        public const String SAM_ACCOUNT_TYPE = "sAMAccountType";
        public const String SHOW_IN_ADDRESSBOOK = "showInAddressBook";
        public const String LEGACY_EXCHANGE_DN = "legacyExchangeDN";
        public const String USER_PRINCIPAL_NAME = "userPrincipalName";
        public const String EXTENSION = "ipPhone";
        public const String SERVICE_PRINCIPAL_NAME = "servicePrincipalName";
        public const String OBJECT_CATEGORY = "objectCategory";
        public const String DS_CORE_PROPAGATION_DATA = "dSCorePropagationData";
        public const String LASTLOGON_TIMESTAMP = "lastLogonTimestamp";
        public const String MANAGER = "manager";
        public const String PAGER = "pager";
        public const String HOME_PHONE = "homePhone";
        public const String MS_EXCH_USER_ACCOUNT_CONTROL = "msExchUserAccountControl";
        public const String MDB_USE_DEFAULTS = "mDBUseDefaults";
        public const String HOME_MDB = "homeMDB";
        public const String MS_EXCH_POLICIES_INCLUDED = "msExchPoliciesIncluded";
        public const String HOME_MTA = "homeMTA";
        public const String MAIL_NICKNAME = "mailNickname";
        public const String MS_EXCH_MAILBOX_SECURITY_DESCRIPTOR = "msExchMailboxSecurityDescriptor";
        public const String MS_EXCH_HOME_SERVER_NAME = "msExchHomeServerName";
        public const String MS_EXCH_RECIPIENT_TYPE_DETAILS = "msExchRecipientTypeDetails";
        public const String MS_EXCH_VERSION = "msExchVersion";
        public const String MS_EXCH_RECIPIENT_DISPLAY_TYPE = "msExchRecipientDisplayType";
        public const String MS_EXCH_MAILBOX_GUID = "msExchMailboxGuid";
        public const String NT_SECURITY_DESCRIPTOR = "nTSecurityDescriptor";
        public const String ALT_SECURITY_IDENTITIES = "altSecurityIdentities";
        public const String PASSWORD_LAST_SET = "pwdLastSet";
        public const String DESCRIPTION = "description";

        /// <summary>
        /// Active Directory API call constants. See <see cref="oActiveDirectory"/> class for usage.
        /// </summary>
        public enum UserAccountControl {
            /// <summary>
            /// Execute the logon script
            /// </summary>
            ADS_UF_SCRIPT = 0x00000001,
            /// <summary>
            /// Diable the account
            /// </summary>
            ADS_UF_ACCOUNTDISABLE = 0x00000002,
            /// <summary>
            /// Requires a root directory
            /// </summary>
            ADS_UF_HOMEDIR_REQUIRED = 0x00000008,
            /// <summary>
            /// Account is locked
            /// </summary>
            ADS_UF_LOCKOUT = 0x00000010,
            /// <summary>
            /// No password is required.
            /// </summary>
            ADS_UF_PASSWD_NOTREQD = 0x00000020,
            /// <summary>
            /// The user cannot change the password.
            /// </summary>
            ADS_UF_PASSWD_CANT_CHANGE = 0x00000040,
            /// <summary>
            /// Encrypted password is allowed.
            /// </summary>
            ADS_UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0x00000080,
            /// <summary>
            /// Local user account for users who's primary account is in another domain.
            /// </summary>
            ADS_UF_TEMP_DUPLICATE_ACCOUNT = 0x00000100,
            /// <summary>
            /// Typical user account
            /// </summary>
            ADS_UF_NORMAL_ACCOUNT = 0x00000200,
            /// <summary>
            /// This is a permit to trust account for a system domain that trusts other domains.
            /// </summary>
            ADS_UF_INTERDOMAIN_TRUST_ACCOUNT = 0x00000800,
            /// <summary>
            /// This is a computer account for a Windows 2000 Professional or Windows 2000 Server that is a member of this domain.
            /// </summary>
            ADS_UF_WORKSTATION_TRUST_ACCOUNT = 0x00001000,
            /// <summary>
            /// This is a computer account for a system backup domain controller that is a member of this domain.
            /// </summary>
            ADS_UF_SERVER_TRUST_ACCOUNT = 0x00002000,
            /// <summary>
            /// When set, the password will not expire on this account.
            /// </summary>
            ADS_UF_DONT_EXPIRE_PASSWD = 0x00010000,
            /// <summary>
            /// This is an Majority Node Set (MNS) logon account. With MNS, you can configure a multi-node Windows cluster without using a common shared disk.
            /// </summary>
            ADS_UF_MNS_LOGON_ACCOUNT = 0x00020000,
            /// <summary>
            /// When set, this flag will force the user to log on using a smart card.
            /// </summary>
            ADS_UF_SMARTCARD_REQUIRED = 0x00040000,
            /// <summary>
            /// When set, the service account (user or computer account), under which a service runs, is trusted for Kerberos delegation.
            /// </summary>
            ADS_UF_TRUSTED_FOR_DELEGATION = 0x00080000,
            /// <summary>
            /// When set, the security context of the user will not be delegated to a service even if the service account is set as trusted for Kerberos delegation.
            /// </summary>
            ADS_UF_NOT_DELEGATED = 0x00100000,
            /// <summary>
            /// Restrict this principal to use only Data Encryption Standard (DES) encryption types for keys.
            /// </summary>
            ADS_UF_USE_DES_KEY_ONLY = 0x00200000,
            /// <summary>
            /// This account does not require Kerberos preauthentication for logon.
            /// </summary>
            ADS_UF_DONT_REQUIRE_PREAUTH = 0x00400000,
            /// <summary>
            /// The user password has expired. This flag is created by the system using data from the password last set attribute and the domain policy. It is read-only and cannot be set.
            /// </summary>
            ADS_UF_PASSWORD_EXPIRED = 0x00800000,
            /// <summary>
            /// The account is enabled for delegation. This is a security-sensitive setting; accounts with this option enabled should be strictly controlled.
            /// </summary>
            ADS_UF_TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 0x01000000
        }
    }
}
