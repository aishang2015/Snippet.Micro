import AppBar from "@mui/material/AppBar";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import IconButton from "@mui/material/IconButton";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import MenuIcon from '@mui/icons-material/Menu';
import List from "@mui/material/List";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import Collapse from "@mui/material/Collapse";
import { ExpandLess, ExpandMore, VerifiedUser, Computer } from "@mui/icons-material";

import InboxIcon from '@mui/icons-material/MoveToInbox';
import SendIcon from '@mui/icons-material/Send';
import HomeIcon from '@mui/icons-material/Home';
import LowPriorityIcon from '@mui/icons-material/LowPriority';
import MiscellaneousServicesIcon from '@mui/icons-material/MiscellaneousServices';
import ManageHistoryIcon from '@mui/icons-material/ManageHistory';
import SettingsInputComponentIcon from '@mui/icons-material/SettingsInputComponent';
import PersonIcon from '@mui/icons-material/Person';
import BadgeIcon from '@mui/icons-material/Badge';
import GroupsIcon from '@mui/icons-material/Groups';
import LockIcon from '@mui/icons-material/Lock';
import AltRouteIcon from '@mui/icons-material/AltRoute';

import React from "react";
import { Box } from "@mui/system";
import Divider from "@mui/material/Divider";

import { PageRoute } from "../router/route";
import { Link, Route, Routes } from "react-router-dom";

export function LayoutPage() {


    const [openedIndex, setOpenedIndex] = React.useState<Array<string>>(new Array<string>());
    const [selectedIndex, setSelectedIndex] = React.useState("0");

    const handleClick = (index: string) => {
        if (openedIndex.find(v => v === index)) {
            setOpenedIndex(openedIndex.filter(v => v !== index));
        } else {
            setOpenedIndex(openedIndex.concat(index));
        }
    };

    const handleListItemClick = (
        event: React.MouseEvent<HTMLDivElement, MouseEvent>,
        index: string,
    ) => {
        setSelectedIndex(index);
    };

    const logout = () => {
        localStorage.removeItem("loginToken");
        window.location.reload();
    }

    const routeData = [
        {
            index: '0',
            route: '/',
            text: '主页',
            icon: <HomeIcon />
        },
        {
            index: '1',
            text: '网关管理',
            isOpen: false,
            icon: <AltRouteIcon />,
            children: [
                {
                    index: '1-1',
                    route: '/gateway',
                    text: '网关管理',
                    icon: <AltRouteIcon />,
                }
            ]
        },
        {
            index: '2',
            text: '认证中心',
            isOpen: false,
            icon: <VerifiedUser />,
            children: [
                {
                    index: '2-1',
                    route: '/auth/client',
                    text: '客户端管理',
                    icon: <Computer />
                },
                {
                    index: '2-2',
                    route: '/auth/scope',
                    text: '范围管理',
                    icon: <LowPriorityIcon />
                }
            ]
        },
        {
            index: '3',
            text: '服务中心',
            isOpen: false,
            icon: <MiscellaneousServicesIcon />,
            children: [
                {
                    index: '3-1',
                    route: '/service',
                    text: '服务管理',
                    icon: <ManageHistoryIcon />
                }
            ]
        },
        {
            index: '4',
            text: '配置中心',
            isOpen: false,
            icon: <SettingsInputComponentIcon />,
            children: [
                {
                    index: '4-1',
                    route: '/config',
                    text: '配置管理',
                    icon: <SettingsInputComponentIcon />
                }
            ]
        },
        {
            index: '5',
            text: '权限管理',
            isOpen: false,
            icon: <LockIcon />,
            children: [
                {
                    index: '5-1',
                    route: '/access/user',
                    text: '用户管理',
                    icon: <PersonIcon />
                },
                {
                    index: '5-2',
                    route: '/access/role',
                    text: '角色管理',
                    icon: <BadgeIcon />
                },
                {
                    index: '5-3',
                    route: '/access/org',
                    text: '组织管理',
                    icon: <GroupsIcon />
                }
            ]
        },
    ];

    return (
        <>
            <Grid container sx={{ height: '100vh', maxHeight: '100vh', overflow: 'auto' }}>
                <Grid item xs={6} md={3} xl={2} style={{ height: '100vh', overflow: "auto" }}>
                    <Box sx={{ bgcolor: 'rgb(5, 30, 52)', color: 'rgb(102, 157, 246)', height: '100vh', overflow: "auto" }}>
                        <List
                            sx={{ width: '100%', height: '100%', maxWidth: 360, padding: '0' }}
                            component="nav"
                        >
                            <ListItemButton component="a" href="#customized-list">
                                <ListItemIcon sx={{ fontSize: 32, color: "white" }}>🔥</ListItemIcon>
                                <ListItemText
                                    sx={{ my: 0, color: "white" }}
                                    primary="Snippet.Micro"
                                    primaryTypographyProps={{
                                        fontSize: 32,
                                        fontWeight: 'medium',
                                        letterSpacing: 0,
                                    }}
                                />
                            </ListItemButton>
                            <Divider sx={{ color: 'red' }} />
                            {
                                routeData.map(d => {
                                    if (d.children) {
                                        return (
                                            <>
                                                <ListItemButton onClick={() => handleClick(d.index)} sx={{ color: 'white', my: 0.5 }}>
                                                    <ListItemIcon sx={{ color: 'inherit' }}>
                                                        {d.icon}
                                                    </ListItemIcon>
                                                    <ListItemText primary={d.text} />
                                                    {openedIndex.find(v => v === d.index) === undefined ? <ExpandLess /> : <ExpandMore />}
                                                </ListItemButton>
                                                <Collapse in={openedIndex.find(v => v === d.index) !== undefined} timeout="auto" unmountOnExit>
                                                    <List component="div" disablePadding>
                                                        {
                                                            d.children.map(child => {
                                                                return (
                                                                    <ListItemButton component={Link} to={child.route} sx={
                                                                        { pl: 4, color: selectedIndex === child.index ? 'palette.primary.light' : 'white' }
                                                                    } selected={selectedIndex === child.index}
                                                                        onClick={(event: any) => handleListItemClick(event, child.index)}>
                                                                        <ListItemIcon sx={{ color: 'inherit' }}>
                                                                            {child.icon}
                                                                        </ListItemIcon>
                                                                        <ListItemText primary={child.text} primaryTypographyProps={{ fontSize: 14, fontWeight: 'medium' }} />
                                                                    </ListItemButton>);
                                                            })
                                                        }
                                                    </List>
                                                </Collapse>
                                            </>
                                        );
                                    } else {
                                        return (
                                            <ListItemButton component={Link} to={d.route} sx={
                                                { color: selectedIndex === d.index ? 'palette.primary.light' : 'white' }
                                            } selected={selectedIndex === d.index}
                                                onClick={(event: any) => handleListItemClick(event, d.index)}>
                                                <ListItemIcon sx={{ color: 'inherit' }}>
                                                    {d.icon}
                                                </ListItemIcon>
                                                <ListItemText primary={d.text} />
                                            </ListItemButton>
                                        );
                                    }
                                })

                            }
                        </List>
                    </Box>
                </Grid>
                <Grid item xs sx={{ height: '100vh', position: 'relative', overflow: 'auto' }}>
                    <Grid container direction="column" justifyContent="start">
                        <AppBar position="sticky" color="default" >
                            <Toolbar>
                                <IconButton
                                    size="large" edge="start" color="inherit" aria-label="menu"
                                    sx={{ mr: 2 }}>
                                    <MenuIcon />
                                </IconButton>
                                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                                    News
                                </Typography>
                                <Button color="inherit" onClick={logout}>Logout</Button>
                            </Toolbar>
                        </AppBar>
                        <Box>
                            <PageRoute />
                        </Box>
                    </Grid>
                </Grid>
            </Grid>
        </>
    );
}