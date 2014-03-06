osql -E -S BEAST -Q "DROP DATABASE giltdb"
osql -E -S BEAST -Q "CREATE DATABASE giltdb"
osql -E -S BEAST -d giltdb -n -i Schema.sql -o out.log
osql -E -S BEAST -d giltdb -n -i Views.sql -o out.log
osql -E -S BEAST -d giltdb -n -i Functions.sql -o out.log
osql -E -S BEAST -d giltdb -n -i Data.sql -o out.log
osql -E -S BEAST -d giltdb -n -i ..\geo.sql -o out.log
PAUSE