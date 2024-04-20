<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="Lite_TaiMeng.Catalog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="mystyles.css" rel="stylesheet" type="text/css" />
    <link href="~/StyleTable.css" rel="stylesheet" type="text/css" />
    <!-- https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Flexible_Box_Layout/Basic_Concepts_of_Flexbox -->
    <style>
        Select
        {
            font-family: 'Segoe UI' !important;
            font-size: 15px !important;
            vertical-align: middle !important;
            display: inline-flex;
            
        }
    </style>
    <title>台夢車</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar is-fixed-top is-link" role="navigation" aria-label="main navigation">
          <div class="container">
              
            <div class="navbar-brand">
                <a class="navbar-item" href="https://bulma.io" style="font-family:'Microsoft YaHei'; font-size: 1.5em;">台夢車</a>

                <a role="button" class="navbar-burger burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
                  <span aria-hidden="true"></span>
                  <span aria-hidden="true"></span>
                  <span aria-hidden="true"></span>
                </a>
            </div>

            <div id="navbarBasicExample" class="navbar-menu">
                
                <div class="navbar-start">
                  <a class="navbar-item">Catalog</a>
                  <a class="navbar-item">Car Blog</a>   
                </div>
                
                <div class="navbar-end">
                  <div class="navbar-item">
                    <div class="buttons">
                      <a class="button is-primary"><strong>Contact Dealer</strong></a>
                    </div>
                  </div>
                </div>
            </div>

          </div> 
        </nav>
        
       
        
        <div class="container notification" style="margin-top: 75px">
            <p class="title is-3">TaiMeng | Taiwan Dream Car</p>
            <p class="subtitle is-5">Find information about your dream car in second</p>
            <p>&nbsp;</p>
            <p>
                List by Brand:
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            &nbsp;&nbsp;
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" Text=" Length View" />
                </p>
            <p>&nbsp;</p>
        </div>
        
        <div class="container">
            <ul class="responsive-table">
                <li class="table-header notification is-success">
                    <div class="col col-0">Image</div>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                    <div class="col col-1">Brand</div>
                    <div class="col col-2">Car Model</div>
                    <div class="col col-3">Style</div>
                    <div class="col col-4">Price Range</div>
                    <div class="col col-5">Fuel Efficiency</div>
                    <div class="col col-6">Engine</div>    
                    <div class="col col-7">CC-Power</div>
                    <div class="col col-8">Transmission</div>
                    <div class="col col-9">Seating</div>
                    <div class="col col-10">More Info</div>
                </li>
            <!-- https://www.dotnetperls.com/stringbuilder -->
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </ul>
           
       </div>
             
        <footer class="footer">
            <div class="content has-text-centered">
            <p>
            <strong><span style="font-family: 'Microsoft YaHei'">台夢車</span> | TaiMeng v.0.5 (Lite)</strong> by 何強豪 Bagas Pram TCUST Marketing Dept. Student. 
            The website content is created for <span style="font-family: 'Microsoft YaHei'">計算機概論課 。</span>
            </p>
            <p><strong>2020. All the Hard Work reserved.</strong></p>
            </div>
        </footer>

        <script src="https://unpkg.com/ionicons@5.0.0/dist/ionicons.js"></script>

    </form>
</body>
</html>
