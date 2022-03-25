import Box from "@mui/material/Box";
import CssBaseline from "@mui/material/CssBaseline";
import FormControlLabel from "@mui/material/FormControlLabel";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import TextField from "@mui/material/TextField";
import Button from '@mui/material/Button';
import Checkbox from '@mui/material/Checkbox';
import Avatar from '@mui/material/Avatar';
import MiscellaneousServicesIcon from '@mui/icons-material/MiscellaneousServices';
import Typography from "@mui/material/Typography";
import React from "react";

function LoginPage() {


    function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        console.log(data);
        localStorage.setItem("loginToken", "abc");
        window.location.reload();
    }

    return (
        <Grid container sx={{ height: '100vh' }}>
            <CssBaseline />
            <Grid item xs={false} sm={4} md={7}
                sx={{
                    backgroundImage: 'url(https://source.unsplash.com/random)',
                    backgroundRepeat: 'no-repeat',
                    backgroundColor: (t) => t.palette.mode === 'light' ? t.palette.grey[50] : t.palette.grey[900],
                    backgroundSize: 'cover',
                    backgroundPosition: 'center',
                }}
            />
            <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6}>
                <Grid
                    container
                    direction="column"
                    justifyContent="center"
                    alignItems="center"
                    style={{ height: '100%' }}
                >
                    <Typography component="h1" variant="h5" sx={{ fontSize: '2em', mb: 2 }}>
                        <MiscellaneousServicesIcon color="primary" sx={{ fontSize: '2em', verticalAlign: "middle", mr: 2 }} />
                        Snippet Micro 微服务系统
                    </Typography>
                    <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 1 }}>
                        <TextField
                            margin="normal" required fullWidth
                            id="email" label="用户名" name="email"
                            autoComplete="off" autoFocus />
                        <TextField
                            margin="normal" required fullWidth
                            name="password" label="密码" type="password"
                            id="password" autoComplete="current-password" />
                        <Button
                            type="submit" fullWidth variant="contained"
                            sx={{ mt: 3, mb: 2 }}>
                            登录</Button>
                    </Box>
                </Grid>
            </Grid>
        </Grid>

    );
}

export { LoginPage };