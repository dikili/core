import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberListComponent } from './member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
{path: 'home', component: HomeComponent},
{
path: '',
runGuardsAndResolvers: 'always',
canActivate: [AuthGuard],
children: [
    {path: 'lists', component: ListsComponent},
    {path: 'members', component: MemberListComponent},
    {path: 'messages', component: MessagesComponent}
]
},

{path: '**', redirectTo: 'home', pathMatch: 'full'}
];

// if not children used it could have been used as below to implement the authentication on the URL
// {path: 'lists', component: ListsComponent, canActivate: [AuthGuard]},
// {path: 'members', component: MemberListComponent, canActivate: [AuthGuard]},
// {path: 'messages', component: MessagesComponent, canActivate: [AuthGuard] }
