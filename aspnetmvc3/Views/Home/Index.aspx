@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

@model IEnumerable<aspnetmvc3.Models.User>

<h2>Index</h2>

<table>
<tr>
<th>User ID</th>
<th>User Name</th>
<th>Email</th>
</tr>

@foreach (var item in Model) {
	<tr>
		<td>@Html.DisplayFor(x =>  item.UserID)</td>
		<td>@Html.DisplayFor(x => item.UserName)</td>
		<td>@Html.DisplayFor(x => item.UserEmail)</td>
	</tr>
}

</table>

