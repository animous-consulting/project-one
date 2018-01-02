# PengKep

Pastikan EntityFramework latest version teristall, dengan cara mengetikkan perintah berikut di Package Manager Console

PM> Install-Package EntityFramework

In the Reference Manager, go to Assemblies -> Extensions and click on: MySql.Data, MySql.Data.Entity.EF6 and MySql.Web

### Change the connection string in web.config:

<pre class="brush: xml;">
&lt;connectionStrings&gt;<br />  &lt;add name=&quot;DefaultConnection&quot;<br />       connectionString=&quot;server=localhost;User Id=user1;Password=123456;Persist Security Info=True;Database=pengkep&quot;<br />       providerName=&quot;MySql.Data.MySqlClient&quot; /&gt;<br />&lt;/connectionStrings&gt;
</pre>

### Change membership:

<pre class="brush: xml;">
&lt;system.web&gt;<br /><br />....<br /><br />&lt;membership defaultProvider=&quot;MySqlMembershipProvider&quot;&gt;<br />    &lt;providers&gt;<br />      &lt;add name=&quot;MySqlMembershipProvider&quot; <br />           type=&quot;MySql.Web.Security.MySqlMembershipProvider, MySql.Web, Version=6.9.5.0,  Culture=neutral, PublicKeyToken=c5687fc88969c44d&quot;<br />           autogenerateschema=&quot;true&quot; <br />           connectionStringName=&quot;DefaultConnection&quot; <br />           enablePasswordRetrieval=&quot;false&quot; <br />           enablePasswordReset=&quot;true&quot; <br />           requiresQuestionAndAnswer=&quot;false&quot; <br />           requiresUniqueEmail=&quot;false&quot; passwordFormat=&quot;Hashed&quot; <br />           maxInvalidPasswordAttempts=&quot;5&quot; <br />           minRequiredPasswordLength=&quot;6&quot; <br />           minRequiredNonalphanumericCharacters=&quot;0&quot; <br />           passwordAttemptWindow=&quot;10&quot; <br />           passwordStrengthRegularExpression=&quot;&quot; <br />           applicationName=&quot;/&quot; /&gt;<br />    &lt;/providers&gt;<br />  &lt;/membership&gt;<br /><br />...<br /><br />&lt;/system.web&gt;
</pre>

### Change Profile

<pre class="brush: xml;">
&lt;system.web&gt;<br /><br />...<br /><br />    &lt;profile&gt;<br />      &lt;providers&gt;<br />        &lt;clear /&gt;<br />        &lt;add name=&quot;MySqlProfileProvider&quot; <br />             type=&quot;MySql.Web.Profile.MySqlProfileProvider, MySql.Web, Version=6.9.5.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d&quot; <br />             applicationName=&quot;/&quot; <br />             description=&quot;&quot; <br />             connectionStringName=&quot;DefaultConnection&quot; <br />             writeExceptionsToEventLog=&quot;True&quot; <br />             autogenerateschema=&quot;True&quot; <br />             enableExpireCallback=&quot;False&quot; /&gt;<br />      &lt;/providers&gt;<br />    &lt;/profile&gt;<br /><br />...<br /><br />&lt;/system.web&gt;
</pre>

### Change role manager

<pre class="brush: xml;">
&lt;/system.web&gt;<br /><br />....<br /><br />    &lt;roleManager enabled=&quot;true&quot; defaultProvider=&quot;MySqlRoleProvider&quot;&gt;<br />        &lt;providers&gt;<br />            &lt;clear /&gt;<br />            &lt;add name=&quot;MySqlRoleProvider&quot;<br />                type=&quot;MySql.Web.Security.MySqlRoleProvider, MySql.Web, Version=6.9.5.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d&quot;<br />                applicationName=&quot;/&quot;<br />                description=&quot;&quot;<br />                connectionStringName=&quot;DefaultConnection&quot;<br />                writeExceptionsToEventLog=&quot;True&quot;<br />                autogenerateschema=&quot;True&quot;<br />                enableExpireCallback=&quot;False&quot; /&gt;<br />        &lt;/providers&gt;<br />    &lt;/roleManager&gt;<br /><br />....<br /><br />&lt;/system.web&gt;
</pre>

### Change old Entity Framework to:

<pre class="brush: xml;">
&lt;entityFramework codeConfigurationType=&quot;MySql.Data.Entity.MySqlEFConfiguration, MySql.Data.Entity.EF6&quot;&gt;<br />    &lt;providers&gt;<br />        &lt;provider invariantName=&quot;MySql.Data.MySqlClient&quot;<br />            type=&quot;MySql.Data.MySqlClient.MySqlProviderServices,<br />            MySql.Data.Entity.EF6, Version=6.9.5.0,<br />            Culture=neutral, PublicKeyToken=c5687fc88969c44d&quot; /&gt;<br />    &lt;/providers&gt;<br />&lt;/entityFramework&gt;
</pre>

<pre class="brush: xml;">
&lt;system.data&gt;<br />    &lt;DbProviderFactories&gt;<br />        &lt;remove invariant=&quot;MySql.Data.MySqlClient&quot; /&gt;<br />        &lt;add name=&quot;MySQL Data Provider&quot; <br />            invariant=&quot;MySql.Data.MySqlClient&quot; <br />            description=&quot;.Net Framework Data Provider for MySQL&quot; <br />            type=&quot;MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Version=6.9.5.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d&quot; /&gt;<br />    &lt;/DbProviderFactories&gt;<br />&lt;/system.data&gt;
</pre>


PM> Enable-Migrations –EnableAutomaticMigrations

Setelah ketik perintah di atas, maka akan terbuatlah folder Migrations

### Edit Configuration.cs

### Edit MySqlHistoryContext.cs

### Tambahkan file MySqlInitializer.cs