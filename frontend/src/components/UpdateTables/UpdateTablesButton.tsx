import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Typography,
} from "@mui/material";
import { useState } from "react";

function UpdateTablesButton({ confirmUpdate }: { confirmUpdate: () => void }) {
  const [openDialog, setOpenDialog] = useState(false); // Controle do estado do diálogo

  const handleUpdate = () => {
    setOpenDialog(true);
  };

  const handleConfirmUpdate = async () => {
    if (confirmUpdate) confirmUpdate();
    setOpenDialog(false);
  };

  const handleCancelUpdate = () => {
    setOpenDialog(false);
  };

  return (
    <div>
      <Button variant="contained" color="primary" onClick={handleUpdate}>
        Update Tables
      </Button>

      <Dialog open={openDialog} onClose={handleCancelUpdate}>
        <DialogTitle>Confirm Update</DialogTitle>
        <DialogContent>
          <Typography variant="body1">
            Are you sure you want to update the tables? This may take some time.
          </Typography>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCancelUpdate} color="primary">
            Cancel
          </Button>
          <Button onClick={handleConfirmUpdate} color="primary">
            Confirm
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default UpdateTablesButton;
