import { Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import FoodDetailsPage from "./pages/FoodDetailsPage";

function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/food/:code" element={<FoodDetailsPage />} />
    </Routes>
  );
}

export default App;
