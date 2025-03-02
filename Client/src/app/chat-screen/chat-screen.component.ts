import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { ChatService } from '../services/chat.service';
import * as signalR from '@microsoft/signalr';
import { Chat } from '../Models/Chat';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BaseUrl } from '../config/environment';
import { AuthService } from '../services/auth.service';
import { TokenService } from '../services/token.service';

@Component({
  selector: 'app-chat-screen',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat-screen.component.html',
  styleUrl: './chat-screen.component.css',
})
export class ChatScreenComponent {
  message: string = '';
  name: string = '';
  private chatHub!: signalR.HubConnection;

  constructor(public AS: AuthService, private readonly TS: TokenService) {
    this.chatHub = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(BaseUrl + 'hubs/chat', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();

    let data: any = TS.GetData();
    let senderId: any = Object.entries(data).find((a) =>
      a[0].includes('claims/nameidentifier') ? a[1] : ''
    );

    this.name =
      data['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    this.send = senderId[1];
  }

  send: string = '';
  rec: string = '';
  chats: any = [];

  formatDate = (ts: Date): string => {
    let currTime = new Date(ts).toLocaleTimeString();
    return currTime;
  };

  chatService = inject(ChatService);

  handleScroll() {
    document
      .querySelectorAll('.messageItem')
      .forEach((a) => a.scrollIntoView({ behavior: 'smooth', block: 'end' }));
  }

  ngOnInit() {
    this.chatHub
      .start()
      .then(() => {
        console.log('SignalR connection has started!');

        this.chatHub.on('ReceiveMessage', (data) => {
          this.chats = JSON.parse(data).filter(
            (chat: Chat) =>
              (chat.senderId == this.send && chat.recieverId == this.rec) ||
              (chat.senderId == this.rec && chat.recieverId == this.send)
          );

          setTimeout(() => {
            this.handleScroll();
          }, 10);
        });
      })
      .catch((err) => {
        console.log('Something went wrong !', err);
      });
  }

  HandleUserChage(e: Event) {
    let uid = (e.target as HTMLInputElement).value;

    if (!!uid) {
      this.rec = uid;
    } else this.rec = '';

    if (!!this.send && !!this.rec) {
      this.chatHub.send('LoadSepecific', this.rec);
    }
  }

  sendMessage = () => {
    this.chatHub
      .send('SendPrivateMessage', this.send, this.message, this.rec)
      .then(() => {
        this.message = '';
      });
  };
}
