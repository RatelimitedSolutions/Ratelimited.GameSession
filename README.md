# GamingSession
goal: Play easy with friends by only using one command.

## mvp
We have a discordbot that handels commands => C#
storing data, sessions, etc => mongodb
server hosting  => digitalocean
game server installer => linuxgsm

### Discord Bot
    cmds: 
        - "/gs new", creates minecraft server
        - "/gs", on top shows commands
        - "/gs sessions", list with minecraft servers
        - "/gs end {id}", end the minecraft server
### Database
    collection:
        - discord[id=matches guild id, session=[ip,port]

### Important
    ssh-keygen -t rsa -b 2048 -m pem -f ~/.ssh/key -C username
    for the ssh key