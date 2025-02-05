import { useState, useEffect, useRef } from "react";
import {
  Typography,
  Dialog,
  DialogContent,
  DialogActions,
  Button,
} from "@mui/material";
import { useUpdateFoods } from "../../hooks/scraper/useUpdateTables";
import CircularWithValueLabel from "./CircularProgressWithLabel";

function UpdateTables({ onClose }: { onClose: () => void }) {
  const { updateFoods, loading, message, error } = useUpdateFoods();
  const [open, setOpen] = useState(true);
  const hasFetched = useRef(false);

  useEffect(() => {
    const fetchData = async () => {
      if (hasFetched.current) return;
      hasFetched.current = true;
      try {
        console.log("chamou UpdateTables()");
        await updateFoods();
      } catch (err) {
        console.error("Error updating:", err);
      }
    };

    fetchData();
  }, [updateFoods]);

  const handleClose = () => {
    if (!loading) {
      setOpen(false);
      if (onClose) onClose();
    }
  };

  return (
    <Dialog
      open={open}
      onClose={loading ? undefined : handleClose}
      disableEscapeKeyDown={loading}
    >
      <DialogContent
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          gap: 2,
        }}
      >
        {loading ? (
          <>
            <CircularWithValueLabel />
            <Typography variant="body1">Updating tables...</Typography>
          </>
        ) : (
          <>
            <Typography
              color={error ? "error.dark" : "success.dark"}
              variant="body1"
            >
              {error || message}
            </Typography>
          </>
        )}
      </DialogContent>
      {!loading && (
        <DialogActions>
          <Button onClick={handleClose} autoFocus>
            OK
          </Button>
        </DialogActions>
      )}
    </Dialog>
  );
}

export default UpdateTables;
