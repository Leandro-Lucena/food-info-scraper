import { Divider, Switch, Typography } from "@mui/material";
import FoodTable from "../components/FoodTable/FoodTable";
import UpdateTablesButton from "../components/UpdateTables/UpdateTablesButton";
import { useContext, useState } from "react";
import UpdateTables from "../components/UpdateTables/UpdateTables";
import { ThemeContext } from "../contexts/ThemeContext";
import { DarkMode, LightMode } from "@mui/icons-material";

function HomePage() {
  const [openUpdate, setOpenUpdate] = useState(false);
  const [foodTableKey, setFoodTableKey] = useState(0);
  const themeContext = useContext(ThemeContext);

  if (!themeContext) return null;
  const { toggleTheme, isDarkMode } = themeContext;

  return (
    <div
      style={{
        paddingBottom: 20,
        width: "80%",
        margin: "0 auto",
        display: "flex",
        gap: 5,
        justifyContent: "center",
        flexDirection: "column",
      }}
    >
      <Typography
        variant="h4"
        sx={{
          padding: 2,
          position: "relative",
        }}
      >
        Food Information Table
        <span style={{ position: "absolute", top: 20, right: 20 }}>
          <LightMode fontSize="small" />
          <Switch checked={isDarkMode} onChange={toggleTheme} />
          <DarkMode fontSize="small" />
        </span>
        <span style={{ position: "absolute", bottom: -72, right: 20 }}>
          <UpdateTablesButton confirmUpdate={() => setOpenUpdate(true)} />
        </span>
      </Typography>
      <Divider variant="middle" />
      <FoodTable key={foodTableKey} />
      {openUpdate && (
        <UpdateTables
          onClose={() => {
            setOpenUpdate(false);
            setFoodTableKey((prevKey) => prevKey + 1);
          }}
        />
      )}
    </div>
  );
}

export default HomePage;
