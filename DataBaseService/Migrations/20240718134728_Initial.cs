using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dt = table.Column<int>(type: "int", nullable: false),
                    sunrise = table.Column<int>(type: "int", nullable: false),
                    sunset = table.Column<int>(type: "int", nullable: false),
                    temp = table.Column<double>(type: "float", nullable: false),
                    feels_like = table.Column<double>(type: "float", nullable: false),
                    pressure = table.Column<int>(type: "int", nullable: false),
                    humidity = table.Column<int>(type: "int", nullable: false),
                    dew_point = table.Column<double>(type: "float", nullable: false),
                    uvi = table.Column<double>(type: "float", nullable: false),
                    clouds = table.Column<int>(type: "int", nullable: false),
                    visibility = table.Column<int>(type: "int", nullable: false),
                    wind_speed = table.Column<double>(type: "float", nullable: false),
                    wind_deg = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalNames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ascii = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    af = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ab = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    feature_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    wo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    av = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    an = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ht = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ff = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    oc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    yi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    yo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ny = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ky = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    so = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    et = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    na = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    be = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    to = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    om = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    am = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    si = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    it = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    io = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    my = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    wa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    es = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    se = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    th = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    st = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    de = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    co = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    te = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ln = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    az = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    li = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    su = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    or = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    br = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    @is = table.Column<string>(name: "is", type: "nvarchar(max)", nullable: false),
                    da = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    km = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    os = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    el = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ig = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    he = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    iu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    oj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APi_key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APi_RasPi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Theme = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tanks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lenght = table.Column<int>(type: "int", nullable: false),
                    Widht = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    FillLevel = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherRoots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lat = table.Column<double>(type: "float", nullable: false),
                    lon = table.Column<double>(type: "float", nullable: false),
                    timezone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timezone_offset = table.Column<int>(type: "int", nullable: false),
                    currentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRoots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherRoots_Currents_currentId",
                        column: x => x.currentId,
                        principalTable: "Currents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    local_namesId = table.Column<long>(type: "bigint", nullable: false),
                    lat = table.Column<double>(type: "float", nullable: false),
                    lon = table.Column<double>(type: "float", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationInfos_LocalNames_local_namesId",
                        column: x => x.local_namesId,
                        principalTable: "LocalNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hourlies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dt = table.Column<int>(type: "int", nullable: false),
                    temp = table.Column<double>(type: "float", nullable: false),
                    feels_like = table.Column<double>(type: "float", nullable: false),
                    pressure = table.Column<int>(type: "int", nullable: false),
                    humidity = table.Column<int>(type: "int", nullable: false),
                    dew_point = table.Column<double>(type: "float", nullable: false),
                    uvi = table.Column<double>(type: "float", nullable: false),
                    clouds = table.Column<int>(type: "int", nullable: false),
                    visibility = table.Column<int>(type: "int", nullable: false),
                    wind_speed = table.Column<double>(type: "float", nullable: false),
                    wind_deg = table.Column<int>(type: "int", nullable: false),
                    wind_gust = table.Column<double>(type: "float", nullable: false),
                    pop = table.Column<double>(type: "float", nullable: false),
                    WeatherRootId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hourlies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hourlies_WeatherRoots_WeatherRootId",
                        column: x => x.WeatherRootId,
                        principalTable: "WeatherRoots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Minutelies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dt = table.Column<int>(type: "int", nullable: false),
                    precipitation = table.Column<int>(type: "int", nullable: false),
                    WeatherRootId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minutelies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Minutelies_WeatherRoots_WeatherRootId",
                        column: x => x.WeatherRootId,
                        principalTable: "WeatherRoots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    main = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentId = table.Column<long>(type: "bigint", nullable: true),
                    HourlyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Weathers_Currents_CurrentId",
                        column: x => x.CurrentId,
                        principalTable: "Currents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weathers_Hourlies_HourlyId",
                        column: x => x.HourlyId,
                        principalTable: "Hourlies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hourlies_WeatherRootId",
                table: "Hourlies",
                column: "WeatherRootId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationInfos_local_namesId",
                table: "LocationInfos",
                column: "local_namesId");

            migrationBuilder.CreateIndex(
                name: "IX_Minutelies_WeatherRootId",
                table: "Minutelies",
                column: "WeatherRootId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRoots_currentId",
                table: "WeatherRoots",
                column: "currentId");

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_CurrentId",
                table: "Weathers",
                column: "CurrentId");

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_HourlyId",
                table: "Weathers",
                column: "HourlyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationInfos");

            migrationBuilder.DropTable(
                name: "Minutelies");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Tanks");

            migrationBuilder.DropTable(
                name: "Weathers");

            migrationBuilder.DropTable(
                name: "LocalNames");

            migrationBuilder.DropTable(
                name: "Hourlies");

            migrationBuilder.DropTable(
                name: "WeatherRoots");

            migrationBuilder.DropTable(
                name: "Currents");
        }
    }
}
